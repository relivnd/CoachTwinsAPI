using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class BaseConfiguration<T>: IEntityTypeConfiguration<T> where T : class
    {

        public BaseConfiguration( )
        {
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {

          
        }
    }
}