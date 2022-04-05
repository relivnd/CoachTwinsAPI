using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwinsApi;
using CoachTwinsApi.Db;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using CoachTwinsAPI.Models.Admin;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoachTwinsAPI.Controllers.Admin
{
    [Route("admin/users")]
    public class UserController: ControllerBase
    {
        [HttpGet("")]
        [LoginRequired(LoginType.PortalUser)]
        public async Task<ActionResult<IList<AdminUser>>> GetAllUsers([FromServices] IUserRepository userRepository, [FromServices] IMapper autoMapper)
        {
            var users = await userRepository.GetAll();
            return Ok(users.Select(autoMapper.Map<AdminUser>));
        }
        
        [HttpGet("{id:guid}")]
        [LoginRequired(LoginType.PortalUser)]
        public async Task<ActionResult<AdminUser>> GetUser([FromServices] IUserRepository userRepository, [FromServices] IMapper autoMapper, Guid id)
        {
            var user = await userRepository.Get(id);

            if (user == null)
                return NotFound();
            return Ok(autoMapper.Map<AdminUser>(user));
        }
        
        [HttpPost("coach/{user:guid}")]
        [LoginRequired(LoginType.PortalUser)]
        public async Task<ActionResult> MakeCoach(Guid user, [FromServices] CoachTwinsDbContext context)
        {
            var entity = context.Students.FirstOrDefault(s => s.Id == user);

            if (entity == null)
                return NotFound();
            
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Students WHERE Id = {0}", user);
            await context.Database.ExecuteSqlRawAsync("INSERT INTO Coaches (Id) VALUES ({0})", user);
            return Ok();
        }

        [HttpPost("student/{user:guid}")]
        [LoginRequired(LoginType.PortalUser)]
        public async Task<ActionResult> MakeStudent(Guid user, [FromServices] CoachTwinsDbContext context)
        {
            var entity = context.Coaches.FirstOrDefault(s => s.Id == user);

            if (entity == null)
                return NotFound();
            
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Coaches WHERE Id = {0}", user);
            await context.Database.ExecuteSqlRawAsync("INSERT INTO Students (Id) VALUES ({0})", user);
            return Ok();
        }
    }
}