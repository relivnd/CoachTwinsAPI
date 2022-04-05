using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class UserConfig: BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            base.Configure(builder);
            builder.HasKey(e => e.Id);

            builder
                .HasMany(e => e.MatchingCriteria)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(e => e.ProfilePicture);
        }

        public UserConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}