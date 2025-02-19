using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UsersEntity>
    {
        public void Configure(EntityTypeBuilder<UsersEntity> builder)
        {
            builder.ToTable("users");
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.DataUsers)
                .WithOne(a => a.Users)
                .HasForeignKey<DataUsersEntity>(a => a.Id);

            builder.HasMany(a => a.Cards)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(a => a.BankScores)
                .WithOne(a => a.Users);
        }
    }
}
