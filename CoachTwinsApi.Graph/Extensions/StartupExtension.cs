using Microsoft.Extensions.DependencyInjection;

namespace CoachTwinsApi.Graph.Extensions
{
    public static class StartupExtension
    {
        public static void AddGraphServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<AgendaManager>();
        }
    }
}