using Microsoft.EntityFrameworkCore;
using PalmArchive.Models;

namespace PalmArchive.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}
