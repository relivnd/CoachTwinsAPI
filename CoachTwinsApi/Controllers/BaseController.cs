using System;
using System.Threading.Tasks;
using AutoMapper;
using CoachTwinsAPI.Auth;
using CoachTwinsApi.Db.Entities;
using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Web;
using CoachTwinsApi;

namespace CoachTwinsAPI.Controllers
{
    public class BaseController: Controller
    {
        protected readonly IStudentRepository StudentRepository;
        protected readonly ICoachRepository CoachRepository;
        protected readonly IUserRepository UserRepository;
        protected readonly IAuthRepository AuthRepository;
        protected readonly IPortalUserRepository PortalUserRepository;
        protected readonly IMatchingRepository matchingRepository;
        protected readonly IMapper Mapper;
        private readonly AuthStore _authStore;


        public BaseController(IStudentRepository studentRepository, ICoachRepository coachRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository, AuthStore authStore,IPortalUserRepository portalRepo,IMatchingRepository matchingRepo)
        {
            StudentRepository = studentRepository;
            CoachRepository = coachRepository;
            UserRepository = userRepository;
            Mapper = mapper;
            _authStore = authStore;
            AuthRepository = authRepository;
            PortalUserRepository = portalRepo;
            matchingRepository = matchingRepo;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = HttpContext.User.GetObjectId();
            
            _authStore.Guid = id == null ? null : Guid.Parse(id);
            
            await base.OnActionExecutionAsync(context, next);
        }
        [LoginRequired]
        protected async Task<T?> GetCurrentUser<T>() where T : User
        {
            var token = (string)HttpContext.Request.Headers["Authorization"];
            token = token.Split("Bearer ")[1];
            var currentUser = await AuthRepository.GetCurrentUser(token);
            if (currentUser == null)
            {
                throw new Exception("User not found");
            }
            return currentUser as T;
        }
    }
}