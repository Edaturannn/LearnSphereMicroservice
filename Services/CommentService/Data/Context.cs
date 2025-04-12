using Microsoft.EntityFrameworkCore;
using Entities.Concrete.CommentService;
namespace CommentService.Data
{
    public class Context : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CommentServiceDatabase;Username=myuser;Password=mypassword");
        }
        public DbSet<Comment> Comments { get; set; }
    }
}