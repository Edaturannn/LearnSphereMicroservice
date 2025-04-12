using Entities.Concrete.CatalogService;
namespace Dtos.Dtos.CatalogDtos.CourseTagDtos;

public class CreateCourseTagDto
{
    public int TagId { get; set; }

    public Tag? Tag { get; set; }
}