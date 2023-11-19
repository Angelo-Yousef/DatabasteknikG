using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabasteknikG.Entities;

internal class ProductEntity
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDescription { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal ProductPrice { get; set; }
    public int PricingUnitId { get; set; }
    public PricingUnitEntity PricingUnit { get; set; } = null!;

    public int ProductCategoryId { get; set; }
    public ProductCategoryEntity ProductCategory { get; set; } = null!;


}