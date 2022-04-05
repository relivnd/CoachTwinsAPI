using CoachTwinsApi.Db.Configuration;
using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachTwinsApi.Db
{
    public class CoachTwinsDbContext : DbContext
    {
        private readonly EncryptionConverterFactory _converterFactory;
        public DbSet<User> Users { get; set; }
        public DbSet<PortalUser> PortalUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MatchingCriteria> MatchingCriteria { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public CoachTwinsDbContext(DbContextOptions options, EncryptionConverterFactory converterFactory) : base(options)
        {
            _converterFactory = converterFactory;
        }
        public CoachTwinsDbContext()
        {
            /*
                cd ./CoachTwinsApi.Db
                dotnet ef --startup-project ../CoachTwinsAPI/ migrations add myMigration01
             dotnet ef --startup-project ../CoachTwinsAPI/ database update
 * 
 drop table __EFMigrationsHistory;
drop table Appointments;
drop table AuthTokens;
drop table Messages;
drop table MatchingCriteria;
drop table Matches;
drop table Criteria;
drop table chats;
drop table matches;
drop table Coaches;
drop table Students;
drop table users;
*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new StudentConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new CoachConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new AppointmentConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new ChatConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new MessageConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new CriteriaConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new AuthConfig(_converterFactory));
            modelBuilder.ApplyConfiguration(new ProfilePictureConfig(_converterFactory));
        }
    }
}