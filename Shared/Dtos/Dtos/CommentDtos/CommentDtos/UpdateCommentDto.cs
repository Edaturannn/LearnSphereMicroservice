namespace Dtos.Dtos.CommentDtos.CommentDtos;

public class UpdateCommentDto
{
    public int Id { get; set; }

    public int UserId { get; set; }    // IdentityService'den
    public int CourseId { get; set; }  // CourseService'den

    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }    // 1–5
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}