namespace Entities.Concrete.PaymentService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }

    public decimal Amount { get; set; }
    public string Status { get; set; } = "Completed"; // veya Failed, Pending

    public DateTime PaidAt { get; set; } = DateTime.UtcNow;
}
