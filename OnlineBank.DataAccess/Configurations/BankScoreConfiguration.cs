using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Configurations
{
    public class BankScoreConfiguration : IEntityTypeConfiguration<BankScoreEntity>
    {
        public void Configure(EntityTypeBuilder<BankScoreEntity> builder)
        {
            builder.ToTable("bankscore");
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Users)
                .WithMany(a => a.BankScores)
                .HasForeignKey(a => a.UsersId);

            builder.HasOne(a => a.Cards)
                .WithOne(a => a.BankScore);
        }
    }
}
