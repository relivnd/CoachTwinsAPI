using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class ProfilePictureConfig: BaseConfiguration<ProfilePicture>
    {
        public override void Configure(EntityTypeBuilder<ProfilePicture> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
        }

        public ProfilePictureConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}