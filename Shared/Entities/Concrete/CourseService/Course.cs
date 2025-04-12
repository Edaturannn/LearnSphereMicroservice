namespace Entities.Concrete.CourseService;
using System.ComponentModel.DataAnnotations;
public class Course
{
    [Key]
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Level { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced
    public int CategoryId { get; set; }
    public int InstructorId { get; set; }

    // Navigation
    public Category? Category { get; set; }
}