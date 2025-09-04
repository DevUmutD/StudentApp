using Microsoft.EntityFrameworkCore;
using StudentApp.Api.Data.Entities;

namespace StudentApp.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<StudentEntity> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentEntity>()
                .HasIndex(s => s.studentNo)
                .IsUnique();
        }
    }
}
