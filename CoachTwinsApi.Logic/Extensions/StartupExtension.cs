using CoachTwinsApi.Logic.Mail;
using CoachTwinsAPI.Logic.Matching;
using Microsoft.Extensions.DependencyInjection;
using CoachTwinsApi.Db.Attribute;

namespace CoachTwinsApi.Logic.Extensions
{
    public static class StartupExtension
    {
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MatchingService>();
            serviceCollection.AddTransient<IMailer, SmtpMailer>();
            serviceCollection.AddTransient<LoginRequiredFilter>();
        }
    }
}