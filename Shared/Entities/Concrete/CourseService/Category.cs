namespace Entities.Concrete.CourseService;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}