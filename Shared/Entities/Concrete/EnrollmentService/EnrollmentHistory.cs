namespace Entities.Concrete.EnrollmentService;
using System.ComponentModel.DataAnnotations;

public class EnrollmentHistory
{
    // EnrollmentService'den gelen enrollment geçmişi bilgisi
    // Bu sınıf, EnrollmentService'den gelen kayıtların geçmişini tutmak için kullanılacak.
    // Örneğin, bir kullanıcının kursa kaydolma durumu değiştiğinde bu değişiklikleri kaydetmek için kullanılabilir.
    [Key]
    public int Id { get; set; }
    
    public int EnrollmentId { get; set; }
    
    // Örneğin, enrollment durumunda yapılan değişiklik (Active → Completed vs.)
    public string OldStatus { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
    
    // Değişikliğin yapıldığı tarih
    public DateTime? ChangeDate { get; set; } 
    
    // Opsiyonel: Değişikliği yapan sistem kullanıcısı veya servis
    public string ChangedBy { get; set; } = string.Empty;

}