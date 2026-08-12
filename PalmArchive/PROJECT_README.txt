PALM ARCHIVE ONLINE STORE - ASP.NET CORE MVC

Built for Visual Studio using ASP.NET Core MVC, Entity Framework Core, SQL Server LocalDB, Razor Views, session cart, and responsive CSS.

FEATURES
- Responsive Palm Archive luxury storefront homepage
- 29 original products seeded from the earlier Palm Archive project
- Browse all products
- Search by product name, brand, category, or tags
- Filter by brand and category
- Sort by price or name
- Product detail page
- Session-based shopping bag
- Instagram ordering link
- Admin product management: create, edit, delete, view
- Side navigation and brand navigation
- SQL Server LocalDB database created automatically on first run

HOW TO RUN
1. Open PalmArchiveBest.slnx in Visual Studio.
2. Wait for NuGet restore.
3. Build Solution.
4. Click the green HTTPS run button or press Ctrl+F5.
5. The database PalmArchiveBestDB is created and seeded automatically. You do NOT need Update-Database.

IMPORTANT
The Instagram links currently point to https://www.instagram.com/. Replace that URL with the real Palm Archive Instagram profile when ready.

MAIN ROUTES
/                  Home
/Products          Shop/search/filter
/Cart              Shopping bag
/Products/Manage   Admin product management
