using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Web;
using CoachTwinsApi;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Controllers
{
    public class BaseController: Controller
    {
        protected readonly IStudentRepository studentRepo;
        protected readonly IAdminRepository adminRepo;
        protected readonly IAuthRepository authRepo;
        protected readonly ICoachRequestRepo coachRequestRepo;


        public BaseController(IStudentRepository userRepository,IAuthRepository authRepo,IAdminRepository adminRepo,ICoachRequestRepo coachRequestRepo)
        {
            this.studentRepo = userRepository;
            this.authRepo = authRepo;
            this.adminRepo = adminRepo;
            this.coachRequestRepo = coachRequestRepo;
        }
        [LoginRequired]
        protected async Task<Student?> GetCurrentStudent() 
        {
            var token = (string)HttpContext.Request.Headers["Authorization"];
            token = token.Split("Bearer ")[1];
            var currentUser = await authRepo.GetCurrentStudent(token);
            if (currentUser == null)
            {
                throw new Exception("User not found");
            }
            return currentUser;
        }
    }
}