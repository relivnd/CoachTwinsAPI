using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class MatchConfig: BaseConfiguration<Match>
    {
        public override void Configure(EntityTypeBuilder<Match> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);

            builder
                .HasMany(e => e.Appointments)
                .WithOne(e => e.Match)
                .HasForeignKey(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(e => e.Chat)
                .WithOne(e => e.Match)
                .HasForeignKey<Chat>(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Student)
                .WithOne(e => e.Match)
                .HasForeignKey<Match>(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public MatchConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}