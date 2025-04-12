namespace Dtos.Dtos.PaymentDtos.PaymentDtos;

public class UpdatePaymentDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }

    public decimal Amount { get; set; }
    public string Status { get; set; } = "Completed"; // veya Failed, Pending

    public DateTime PaidAt { get; set; } = DateTime.UtcNow;
}
