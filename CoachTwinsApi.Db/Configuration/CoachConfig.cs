using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class CoachConfig: BaseConfiguration<Coach>
    {
        public override void Configure(EntityTypeBuilder<Coach> builder)
        {
            base.Configure(builder);
            builder.ToTable("Coaches");

            builder.HasMany(e => e.Matches)
                .WithOne(e => e.Coach)
                .IsRequired(false)
                .HasForeignKey(e => e.CoachId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public CoachConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}