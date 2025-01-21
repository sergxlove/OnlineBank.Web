using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Configurations
{
    public class DataUsersConfiguration : IEntityTypeConfiguration<DataUsersEntity>
    {
        public void Configure(EntityTypeBuilder<DataUsersEntity> builder)
        {
            builder.ToTable("datausers");
            builder.HasKey(a => a.Id);
        }
    }
}
