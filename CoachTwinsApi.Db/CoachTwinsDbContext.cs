using CoachTwinsApi.Db.Configuration;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachTwinsApi.Db
{
    public class CoachTwinsDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<CoachRequest> CoachRequests { get; set; }
        public CoachTwinsDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new AdminConfig());
            modelBuilder.ApplyConfiguration(new AuthConfig());
            modelBuilder.ApplyConfiguration(new CoachRequestConfig());

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

    }
}