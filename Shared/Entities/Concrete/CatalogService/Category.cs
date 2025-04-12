namespace Entities.Concrete.CatalogService;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; } // UI için
}