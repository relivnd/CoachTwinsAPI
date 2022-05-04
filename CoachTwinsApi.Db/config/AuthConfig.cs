using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class AuthConfig: BaseConfiguration<AuthToken>
    {
        public override void Configure(EntityTypeBuilder<AuthToken> builder)
        {
            base.Configure(builder);
           builder.HasKey(s=>s.Id);
        }

    }
}