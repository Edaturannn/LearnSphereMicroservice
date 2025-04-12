using Microsoft.EntityFrameworkCore;
using Entities.Concrete.EnrollmentService;
namespace EnrollmentService.Data
{
    public class Context : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=EnrollmentServiceDatabase;Username=myuser;Password=mypassword");
        }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<EnrollmentHistory> EnrollmentHistories { get; set; }
    }
}