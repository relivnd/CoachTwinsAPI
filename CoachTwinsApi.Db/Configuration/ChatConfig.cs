using CoachTwinsApi.Db.Converters;
using CoachTwinsApi.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoachTwinsApi.Db.Configuration
{
    public class ChatConfig : BaseConfiguration<Chat>
    {
        public override void Configure(EntityTypeBuilder<Chat> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
            
            builder
                .HasMany(e => e.Messages)
                .WithOne(e => e.Chat)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public ChatConfig(EncryptionConverterFactory factory) : base(factory)
        {
        }
    }
}