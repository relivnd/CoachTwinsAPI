using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwins.Models.Matching.Requests;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using FileTypeChecker;
using FileTypeChecker.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers.Profile.Setup
{
    [LoginRequired]
    [ApiController]
    [Route("profile/setup")]
    public class ProfileController : BaseController
    {
        public ProfileController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }

        [HttpGet("status")]
        public async Task<IActionResult> IsPersonalProfileSetup(Guid userId)
        {
            var user = await GetCurrentUser<User>();
            if (user == null)
                return NotFound();


            var statusResult = new {Status = user.IsProfileSetup};

            return Ok(statusResult);
        }

        [HttpPost]
        public async Task SetupProfile(StudentProfileSetupRequest studentProfileSetupRequest,
            [FromServices] ICriteriaRepository criteriaRepository)
        {
            var user = await GetCurrentUser<User>();
            if (user == null || user.IsProfileSetup)
                return;

            var minBirthDate = DateTime.Now.AddYears(-130);
            var maxBirthDate = DateTime.Now.AddYears(-10);

            var currentBirth = studentProfileSetupRequest.ProfileData.BirthDate;

            // age should be within [minBirth, maxBirth]
            if (currentBirth >= minBirthDate && currentBirth <= maxBirthDate)
                user.BirthDate = studentProfileSetupRequest.ProfileData.BirthDate;

            user.Description = studentProfileSetupRequest.ProfileData.Description;

            // if an image was uploaded, we need to upload it into the database.
            if (studentProfileSetupRequest.ProfileData.ProfilePicture != null)
            {
                // verify that the inserted stream is, in fact, an image.
                // only attach the image if it is a valid image file.
                await using var stream = new MemoryStream(studentProfileSetupRequest.ProfileData.ProfilePicture);
                if (FileTypeValidator.IsTypeRecognizable(stream) && stream.IsImage())
                {
                    var pf = new ProfilePicture()
                    {
                        data = studentProfileSetupRequest.ProfileData.ProfilePicture,
                        Id = Guid.NewGuid()
                    };
                    await UserRepository.AddProfilePicture(pf);
                    user.ProfilePicture = pf;
                }
            }

            user.Gender = studentProfileSetupRequest.ProfileData.Gender;
            user.PreviousEducation = studentProfileSetupRequest.ProfileData.PreviousEducation;

            foreach (var criterion in studentProfileSetupRequest.MatchingCriteria)
            {
                var criteria = await criteriaRepository.Get(criterion.Key);

                if (criteria == null)
                    continue;

                user.MatchingCriteria.Add(new MatchingCriteria()
                {
                    Criteria = criteria,
                    Prefer = criterion.Prefer,
                    Value = criterion.Value
                });
            }

            user.IsProfileSetup = true;

            await UserRepository.Update(user);
        }

      
    }
}