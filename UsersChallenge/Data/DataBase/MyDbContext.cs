using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        private static string Schema => "MyDb";

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Location>().ToTable("Locations", Schema);
            modelBuilder.Entity<Location>(entity =>
            {
                //entity.HasKey(e => e.IdValue);
                //entity.HasIndex(e => e.IdValue).IsUnique();
            });

            modelBuilder.Entity<User>().ToTable("Users", Schema);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdValue);
                entity.HasIndex(e => e.IdValue).IsUnique();
                entity.HasOne(e => e.Location)
                    .WithMany(p => p.Users);
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
