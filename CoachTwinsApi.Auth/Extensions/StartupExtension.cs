using Microsoft.Extensions.DependencyInjection;

namespace CoachTwinsAPI.Auth.Extensions
{
    public static class StartupExtension
    {
        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddScoped<AuthStore>();
        }
    }
}