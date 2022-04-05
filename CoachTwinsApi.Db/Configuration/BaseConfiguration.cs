using System;
using System.Linq;
using CoachTwinsApi.Db.Attribute;
using CoachTwinsApi.Db.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class BaseConfiguration<T>: IEntityTypeConfiguration<T> where T : class
    {
        private readonly EncryptionConverterFactory _factory;

        public BaseConfiguration(EncryptionConverterFactory factory)
        {
            _factory = factory;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            var encrypt = typeof(T).GetProperties().Where(p => System.Attribute.IsDefined(p, typeof(Encrypt)));

            foreach (var property in encrypt)
            {
                if (property.PropertyType == typeof(string))
                    builder.Property(property.Name).HasConversion(_factory.Create<string>());
                if (property.PropertyType == typeof(DateTime))
                    builder.Property(property.Name).HasConversion(_factory.Create<DateTime>());
                if (property.PropertyType == typeof(int))
                    builder.Property(property.Name).HasConversion(_factory.Create<int>());
                if (property.PropertyType == typeof(long))
                    builder.Property(property.Name).HasConversion(_factory.Create<long>());
                if (property.PropertyType == typeof(bool))
                    builder.Property(property.Name).HasConversion(_factory.Create<bool>());
            }
        }
    }
}