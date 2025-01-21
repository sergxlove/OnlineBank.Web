using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Configurations
{
    public class CardsConfiguration : IEntityTypeConfiguration<CardsEntity>
    {
        public void Configure(EntityTypeBuilder<CardsEntity> builder)
        {
            builder.ToTable("cards");
            builder.HasKey(a => a.Id);
        }
    }
}
