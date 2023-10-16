using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleChat.Data.Entities;

namespace SimpleChat.Data.Configurations
{
    public class UserChatConfiguration : IEntityTypeConfiguration<UserChatEntity>
    {
        public void Configure(EntityTypeBuilder<UserChatEntity> builder)
        {
            builder.HasKey(uc => uc.Id);
            builder.Property(uc => uc.Id)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();

            builder.Property(uc => uc.UserId)
                .IsRequired();

            builder.Property(uc => uc.ChatId)
                .IsRequired();
        }
    }
}
