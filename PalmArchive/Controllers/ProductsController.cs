using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalmArchive.Data;
using PalmArchive.Models;

namespace PalmArchive.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    public ProductsController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? q, string? brand, string? category, string? sort)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim();
            query = query.Where(p => p.Name.Contains(term) || p.Brand.Contains(term) || p.Category.Contains(term) || p.Tags.Contains(term));
        }
        if (!string.IsNullOrWhiteSpace(brand)) query = query.Where(p => p.Brand == brand);
        if (!string.IsNullOrWhiteSpace(category)) query = query.Where(p => p.Category == category);

        query = sort switch
        {
            "price-low" => query.OrderBy(p => p.Price),
            "price-high" => query.OrderByDescending(p => p.Price),
            "name" => query.OrderBy(p => p.Name),
            _ => query.OrderBy(p => p.Id)
        };

        ViewBag.Brands = await _context.Products.Select(p => p.Brand).Distinct().OrderBy(x => x).ToListAsync();
        ViewBag.Categories = await _context.Products.Select(p => p.Category).Distinct().OrderBy(x => x).ToListAsync();
        ViewBag.Q = q;
        ViewBag.Brand = brand;
        ViewBag.Category = category;
        ViewBag.Sort = sort;
        return View(await query.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? NotFound() : View(product);
    }

    public async Task<IActionResult> Manage() => View(await _context.Products.OrderBy(p => p.Brand).ThenBy(p => p.Name).ToListAsync());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid) return View(product);
        _context.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Manage));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product == null ? NotFound() : View(product);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return NotFound();
        if (!ModelState.IsValid) return View(product);
        _context.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Manage));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product == null ? NotFound() : View(product);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null) { _context.Products.Remove(product); await _context.SaveChangesAsync(); }
        return RedirectToAction(nameof(Manage));
    }
}
