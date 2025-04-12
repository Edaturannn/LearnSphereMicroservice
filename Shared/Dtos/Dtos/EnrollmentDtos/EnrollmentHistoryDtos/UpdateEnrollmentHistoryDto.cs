namespace Dtos.Dtos.EnrollmentDtos.EnrollmentHistoryDtos;

public class UpdateEnrollmentHistoryDto
{   
    public int Id { get; set; }
    
    public int EnrollmentId { get; set; }
    
    // Örneğin, enrollment durumunda yapılan değişiklik (Active → Completed vs.)
    public string OldStatus { get; set; } = string.Empty;
    public string NewStatus { get; set; } = string.Empty;
    
    // Değişikliğin yapıldığı tarih
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow;
    
    // Opsiyonel: Değişikliği yapan sistem kullanıcısı veya servis
    public string ChangedBy { get; set; } = string.Empty;
}