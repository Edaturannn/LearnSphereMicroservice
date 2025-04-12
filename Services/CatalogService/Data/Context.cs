using Microsoft.EntityFrameworkCore;
using Entities.Concrete.CatalogService;
namespace CatalogService.Data
{
    public class Context : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CatalogServiceDatabase;Username=myuser;Password=mypassword");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}