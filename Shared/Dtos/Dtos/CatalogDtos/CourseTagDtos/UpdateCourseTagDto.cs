using Entities.Concrete.CatalogService;
namespace Dtos.Dtos.CatalogDtos.CourseTagDtos;

public class UpdateCourseTagDto
{
    public int CourseId { get; set; }
    public int TagId { get; set; }

    public Tag? Tag { get; set; }
}