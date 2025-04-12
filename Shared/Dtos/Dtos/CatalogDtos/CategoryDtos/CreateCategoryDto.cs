namespace Dtos.Dtos.CatalogDtos.CategoryDtos;

public class CreateCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; } // UI için
}