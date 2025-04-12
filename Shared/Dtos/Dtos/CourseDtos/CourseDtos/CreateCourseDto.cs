namespace Dtos.Dtos.CourseDtos.CourseDtos;

public class CreateCourseDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Level { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced
    public int CategoryId { get; set; }
    public int InstructorId { get; set; }
}