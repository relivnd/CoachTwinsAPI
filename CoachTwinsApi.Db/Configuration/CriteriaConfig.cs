using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class CriteriaConfig: BaseConfiguration<Criteria>
    {
        public override void Configure(EntityTypeBuilder<Criteria> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Category);
        }

        public CriteriaConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}