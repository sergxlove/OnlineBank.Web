using Microsoft.EntityFrameworkCore;
using OnlineBank.DataAccess.Configurations;
using OnlineBank.DataAccess.Models;

namespace OnlineBank.DataAccess
{
    public class DbContextSqlite : DbContext
    {
        public DbContextSqlite()
        {
            Database.EnsureCreated();
        }

        public DbSet<UsersEntity> Users { get; set; }

        public DbSet<DataUsersEntity> DataUsers { get; set; }

        public DbSet<CardsEntity> Cards { get; set; }

        public DbSet<SystemTableEntity> SystemTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=D:\projects\OnlineBank.Web\Data\data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DataUsersConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new CardsConfiguration());
            modelBuilder.ApplyConfiguration(new SystemTableConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
