using AutoMapper;
using CoachTwinsAPI.Auth;
using CoachTwinsAPI.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace CoachTwinsAPI.Extensions.Startup
{
    public static class AutomapperExtension
    {
        public static void ConfigureAutomapper(this IServiceCollection serviceCollection)
        {
            // adding this as scoped instead of singleton because it relies on the scoped AuthStore
            serviceCollection.AddScoped(c =>
            {
                return new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DomainDatabaseMapping(c.GetService<AuthStore>()));
                }).CreateMapper();
            });
        }
    }
}