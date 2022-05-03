using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CoachTwinsApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    // This class is the attribute. Note that it is not an action filter itself.
    // This class cannot have DI constructor injection, but it can access the IServiceProvider.
    public class LoginRequiredAttribute : Attribute, IFilterFactory
    {
        private bool _required = true;
        
        public LoginRequiredAttribute()
        {
        }
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<LoginRequiredFilter>();
        }
    }


    // This class is the actual filter. It is not an attribute.
    // This class *can* have DI constructor injection.
    public class LoginRequiredFilter : IActionFilter // or IAsyncActionFilter
    {

        private IAuthRepository _repo;
        public LoginRequiredFilter(IAuthRepository repo)
        {
            this._repo = repo;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var bearerTokenObj))
            {
                context.Result = UnAuthorizedResult;
                return;
            }
            var bearerToken = (string)bearerTokenObj;
            if (!bearerToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = UnAuthorizedResult;
                return;
            }
            bearerToken = bearerToken["Bearer ".Length..].Trim();
            if (bearerToken is null)
            {
                context.Result = UnAuthorizedResult;
                return;
            }
            var token = await _repo.Check(bearerToken);
            if (token is null)
            {
                context.Result = UnAuthorizedResult;
                return;
            }
            if (token.IsValid == false)
            {
                context.Result = UnAuthorizedResult;
                return;
            }


        }
        private IActionResult UnAuthorizedResult => new UnauthorizedObjectResult("Bearer token not found or invalid");
    }


}

