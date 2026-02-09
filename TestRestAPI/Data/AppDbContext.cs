using Microsoft.EntityFrameworkCore;
using TestRestAPI.Models;
using Microsoft.SqlServer.Server;
namespace TestRestAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Student>().HasData(
                    new Student { Id = 1, Name = "John Doe", Age = 20 },
                    new Student { Id = 2, Name = "Jane Smith", Age = 22 },
                    new Student { Id = 3, Name = "Michael Johnson", Age = 19 }
                );
        }
    }
}
