using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PalmArchive.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Brand { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string Category { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 1000000)]
    public decimal Price { get; set; }

    [Required]
    public string Image { get; set; } = string.Empty;

    [StringLength(600)]
    public string Description { get; set; } = string.Empty;

    [StringLength(500)]
    public string Tags { get; set; } = string.Empty;
}
