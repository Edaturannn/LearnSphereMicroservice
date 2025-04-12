using Microsoft.EntityFrameworkCore;
using Entities.Concrete.PaymentService;
namespace PaymentService.Data
{
    public class Context : DbContext
    {
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PaymentServiceDatabase;Username=myuser;Password=mypassword;Include Error Detail=true");
        }
        public DbSet<Payment> Payments { get; set; }
    }
}