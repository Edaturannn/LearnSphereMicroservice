using Entities.Concrete.CatalogService;
namespace Dtos.Dtos.CatalogDtos.TagDtos;

public class UpdateTagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}