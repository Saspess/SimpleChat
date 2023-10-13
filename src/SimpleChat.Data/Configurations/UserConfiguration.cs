using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleChat.Data.Entities;

namespace SimpleChat.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Username)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
