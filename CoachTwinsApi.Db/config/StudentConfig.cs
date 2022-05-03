using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class StudentConfig: BaseConfiguration<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder);
           builder.HasKey(s=>s.Id);
        }

    }
}