using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess.Configurations
{
    public class SystemTableConfiguration : IEntityTypeConfiguration<SystemTableEntity>
    {
        public void Configure(EntityTypeBuilder<SystemTableEntity> builder)
        {
            builder.ToTable("systemTable");
            builder.HasKey(a => a.Id);
        }
    }
}
