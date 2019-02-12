using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<User>().ToTable("Users", "test");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdValue);
                //entity.HasIndex(e => e.Title).IsUnique();
                //entity.Property(e => e.DateTimeAdd).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
