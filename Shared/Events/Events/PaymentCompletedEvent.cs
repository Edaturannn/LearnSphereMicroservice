namespace Shared.Events
{
    public class PaymentCompletedEvent
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
