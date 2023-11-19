using System.ComponentModel.DataAnnotations;

namespace DatabasteknikG.Entities;

public class ProductCategoryEntity
{
    [Key]
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
}