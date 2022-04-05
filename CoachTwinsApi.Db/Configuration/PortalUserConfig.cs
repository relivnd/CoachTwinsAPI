using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class PortalUserConfig : BaseConfiguration<PortalUser>
    {
        public override void Configure(EntityTypeBuilder<PortalUser> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);

            
        }

        public PortalUserConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}
