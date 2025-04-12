using Microsoft.EntityFrameworkCore;
using Entities.Concrete.CourseService;
namespace CourseService.Data
{
    public class Context : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CourseServiceDatabase;Username=myuser;Password=mypassword");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}