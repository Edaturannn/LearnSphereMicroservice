namespace Entities.Concrete.CourseService;
using System.ComponentModel.DataAnnotations;

public class Instructor
{
    [Key]
    public int InstructorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;

    public List<Course> Courses { get; set; } = new();
}