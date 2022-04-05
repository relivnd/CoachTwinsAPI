using Microsoft.Extensions.DependencyInjection;

namespace CoachTwinsApi.Templating.Extensions
{
    public static class StartupExtension
    {
        public static void AddTemplating(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddRazorPages();
            serviceCollection.AddTransient<TemplateRenderer>();
        }
    }
}