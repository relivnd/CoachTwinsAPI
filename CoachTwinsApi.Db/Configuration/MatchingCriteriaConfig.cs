using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class MatchingCriteriaConfig: BaseConfiguration<MatchingCriteria>
    {
        public override void Configure(EntityTypeBuilder<MatchingCriteria> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Prefer).IsRequired(false);
        }

        public MatchingCriteriaConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}