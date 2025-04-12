using Entities.Concrete.CatalogService;
namespace Dtos.Dtos.CatalogDtos.SubCategoryDtos;

public class CreateSubCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}