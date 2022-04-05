using System.Threading.Tasks;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsApi.Logic.Mail
{
    public interface IMailer
    {
        public Task SendMail(User to, string subject, string template);
        public Task SendMail<T>(User to, string subject, string template, T data);
    }
}