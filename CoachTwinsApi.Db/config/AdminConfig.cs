using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class AdminConfig: BaseConfiguration<Administrator>
    {
        public override void Configure(EntityTypeBuilder<Administrator> builder)
        {
            base.Configure(builder);
           builder.HasKey(s=>s.Id);
        }

    }
}