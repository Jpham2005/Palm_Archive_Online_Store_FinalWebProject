using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PalmArchive.Data;

namespace PalmArchive.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    public HomeController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var featured = await _context.Products.OrderBy(p => p.Id).Take(8).ToListAsync();
        return View(featured);
    }

    public IActionResult Privacy() => View();
}
