using Microsoft.EntityFrameworkCore;
using Gunita.Api.Models;

namespace Gunita.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Arrangement> Arrangements => Set<Arrangement>();
        public DbSet<Atc> Atc => Set<Atc>();
        public DbSet<Status> Status => Set<Status>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("User", "exe");
            modelBuilder.Entity<Arrangement>()
                .ToTable("Arrangement", "exe");
            modelBuilder.Entity<Atc>()
                .ToTable("Atc", "exe");
            modelBuilder.Entity<Status>()
                .ToTable("StatusTable", "exe");
        }
    }
}
