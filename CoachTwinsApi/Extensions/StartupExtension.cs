using CoachTwinsApi.Db.Repository;
using CoachTwinsApi.Db.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoachTwinsApi.Db.Extensions
{
    public static class StartupExtension
    {
        public static void ConfigureEf(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<CoachTwinsDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("CoachTwins"),
                x => x.MigrationsAssembly("CoachTwinsApi.Db")));
           
        }

        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<CoachTwinsDbContext>()
                .AddTransient<IStudentRepository, StudentRepository>()
            .AddScoped<IAdminRepository, AdminRepository>()
            .AddScoped<IAuthRepository, AuthRepository>()
            .AddScoped<ICoachRequestRepo,CoachRequestRepo>()
            .AddScoped<LoginRequiredAttribute>()
            .AddTransient<LoginRequiredFilter>();
        }
    }
}