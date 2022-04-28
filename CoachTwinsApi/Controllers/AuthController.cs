﻿using Microsoft.AspNetCore.Mvc;
using CoachTwinsAPI.Auth;
using System.Threading.Tasks;
using CoachTwinsAPI.Logic.Matching;
using CoachTwinsApi.Db.Repository.Contract;
using AutoMapper;
using System;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Extensions;
using Microsoft.AspNetCore.Cors;
using CoachTwinsApi;
using System.Collections.Generic;
using System.Linq;

namespace CoachTwinsAPI.Controllers
{
    public class LoginObject
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    [ApiController]
    [Route("Auth")]
    public class AuthController : BaseController
    {
        public AuthController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore, IPortalUserRepository portalRepo, IMatchingRepository matchingRepo) : base(studentRepository, coachRepository, userRepository, mapper, authRepository, authStore, portalRepo, matchingRepo)
        {
        }
        /// <summary>
        /// Logs in coach or coachee in the app. If credentials are correct, an AuthToken object is returned, of which the token value has to be included in every header to authenticate the user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>AuthToken</returns>
        [HttpPost("GetToken")]
        public async Task<ActionResult<AuthToken>> GetToken([FromBody] LoginObject login)
        {
            if (login is null)
            {
                return BadRequest("loginobject is null");
            }
            var token = await AuthRepository.Login(login.Email, login.Password.Sha256());
            if (token is not null)
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return Ok(token);
            }
            else
            {
                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return Unauthorized();
            }
        }
        /// <summary>
        /// Checks if the current AuthToken is still valid
        /// </summary>
        /// <param name="token"></param>
        /// <returns>HTTP 200 or 401</returns>
        [HttpGet("CheckToken")]
        public async Task<ActionResult<bool>> CheckToken([FromHeader] string token)
        {
            var tokenObj = await AuthRepository.Check(token);
            return tokenObj is null ? Unauthorized() : Ok();
        }
        /// <summary>
        /// Refreshes the authtoken. Previous authtoken cannot be used after refreshing.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>A new AuthToken</returns>
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<AuthToken>> RefreshToken([FromHeader] string token)
        {
            var newToken = await AuthRepository.Update(token);
            return newToken is not null ? Ok(newToken) : Unauthorized();
        }

        /// <summary>
        /// Gets all active authtokens, for development only
        /// </summary>
        [HttpGet("activeTokens")]
        public async Task<ActionResult<List<(string token, Guid userId)>>> getActiveTokens(){
            var tokens = (await AuthRepository.GetAll())
                .Where(t => t.Active)
                .Select(t => (t.Value, t.ActiveGuid))
                .ToList();

            return tokens;
            }
}
}