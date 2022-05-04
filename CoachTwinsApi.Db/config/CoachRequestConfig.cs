using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class CoachRequestConfig: BaseConfiguration<CoachRequest>
    {
        public override void Configure(EntityTypeBuilder<CoachRequest> builder)
        {
            base.Configure(builder);
           builder.HasKey(s=>s.Id);
        }

    }
}