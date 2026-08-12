using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PalmArchive.Data;
using PalmArchive.Models;

namespace PalmArchive.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private const string Key = "PalmArchiveCart";
    public CartController(ApplicationDbContext context) => _context = context;

    public IActionResult Index() => View(GetCart());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int id, string? returnUrl = null)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        var cart = GetCart();
        var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item == null)
            cart.Add(new CartItem { ProductId = product.Id, Name = product.Name, Brand = product.Brand, Price = product.Price, Image = product.Image, Quantity = 1 });
        else item.Quantity++;
        SaveCart(cart);
        TempData["CartMessage"] = $"{product.Name} added to your bag.";
        return !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Index", "Products");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var cart = GetCart();
        cart.RemoveAll(x => x.ProductId == id);
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        HttpContext.Session.Remove(Key);
        return RedirectToAction(nameof(Index));
    }

    private List<CartItem> GetCart()
    {
        var json = HttpContext.Session.GetString(Key);
        return string.IsNullOrEmpty(json) ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }
    private void SaveCart(List<CartItem> cart) => HttpContext.Session.SetString(Key, JsonSerializer.Serialize(cart));
}
