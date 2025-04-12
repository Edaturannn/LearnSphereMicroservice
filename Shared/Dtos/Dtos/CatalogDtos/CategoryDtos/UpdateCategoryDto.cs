namespace Dtos.Dtos.CatalogDtos.CategoryDtos;

public class UpdateCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; } // UI için
}
