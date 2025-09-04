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
    }
}
