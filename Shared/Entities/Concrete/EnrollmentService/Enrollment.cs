namespace Entities.Concrete.EnrollmentService;
using System.ComponentModel.DataAnnotations;

public class Enrollment
{
    [Key]
    public int Id { get; set; }
    
    // Kullanıcının IdentityService'den gelen unique identifier'ı (örneğin, UserId)
    public int UserId { get; set; }
    
    // CourseService'den gelen kursun Id'si
    public int CourseId { get; set; }
    
    // Kaydolma tarihi
    public DateTime? EnrollmentDate { get; set; }
    
    // Enrollment status: örneğin "Active", "Completed", "Canceled" gibi durumlar
    public string Status { get; set; } = "Active";
    
    // Kursu bitirme durumu (opsiyonel)
    public bool IsCompleted { get; set; } = false;
    
    // Opsiyonel: Kursun bitiş tarihine ilişkin bilgi
    public DateTime? CompletionDate { get; set; }
    
    // Opsiyonel: Ek notlar veya yorumlar
    public string? Notes { get; set; }
}