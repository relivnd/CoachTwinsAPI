using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachTwinsApi.Db.Configuration
{
    internal class ProfileReportConfig: BaseConfiguration<ProfileReport>
    {
     
            public override void Configure(EntityTypeBuilder<ProfileReport> builder)
            {
                base.Configure(builder);
                builder.HasKey(e => e.Id);
            }

            public ProfileReportConfig(EncryptionConverterFactory factory) : base(factory)
            {
            }
    }
}
