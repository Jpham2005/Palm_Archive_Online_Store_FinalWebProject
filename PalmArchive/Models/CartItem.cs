namespace PalmArchive.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal LineTotal => Price * Quantity;
}
