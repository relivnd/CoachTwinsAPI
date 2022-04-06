using CoachTwinsApi.Db.Converters;
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
            // use either "CoachTwins" or "CoachTwinsDev"
            serviceCollection.AddDbContext<CoachTwinsDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("CoachTwinsDev"),
                x => x.MigrationsAssembly("CoachTwinsApi.Db")));
        }

        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<ICoachRepository, CoachRepository>();
            serviceCollection.AddTransient<IStudentRepository, StudentRepository>();
            serviceCollection.AddTransient<ICriteriaRepository, CriteriaRepository>();
            serviceCollection.AddTransient<IMatchingRepository, MatchingRepository>();
            serviceCollection.AddTransient<IChatRepository, ChatRepository>();
            serviceCollection.AddTransient<IMatchingCriteriaRepository, MatchingCriteriaRepository>();
            serviceCollection.AddTransient<IAppointmentRepository, AppointmentRepository>();
            serviceCollection.AddTransient<IAuthRepository, AuthRepository>();
            serviceCollection.AddTransient<IPortalUserRepository, PortalUserRepository>();
            serviceCollection.AddScoped<LoginRequiredAttribute>();
            serviceCollection.AddScoped<EntityEncryptor>();
            serviceCollection.AddScoped<EncryptionConverterFactory>();
        }
    }
}