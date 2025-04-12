namespace Entities.Concrete.CatalogService;
using System.ComponentModel.DataAnnotations;

public class SubCategory
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}