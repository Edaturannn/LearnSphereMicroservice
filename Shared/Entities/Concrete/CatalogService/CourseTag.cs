namespace Entities.Concrete.CatalogService;
using System.ComponentModel.DataAnnotations;

public class CourseTag
{
    [Key]
    public int CourseId { get; set; }
    public int TagId { get; set; }

    public Tag? Tag { get; set; }
}