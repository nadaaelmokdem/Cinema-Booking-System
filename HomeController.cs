using CinemaBookingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Controllers;

// Anyone can browse - no [Authorize] on this controller.
public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET /Movies?categoryId=&search=
    public async Task<IActionResult> Index(int? categoryId, string? search)
    {
        var query = _context.Movies
            .Include(m => m.Category)
            .Where(m => m.IsShowing)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(m => m.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(m => m.Title.Contains(search));
        }

        ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        ViewBag.SelectedCategoryId = categoryId;
        ViewBag.Search = search;

        var movies = await query.OrderBy(m => m.Title).ToListAsync();
        return View(movies);
    }

    // GET /Movies/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var movie = await _context.Movies
            .Include(m => m.Category)
            .Include(m => m.Showtimes)
                .ThenInclude(s => s.Hall)
                    .ThenInclude(h => h!.Cinema)
            .Include(m => m.Showtimes)
                .ThenInclude(s => s.Bookings)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return NotFound();
        }

        // Only show upcoming showtimes, soonest first.
        movie.Showtimes = movie.Showtimes
            .Where(s => s.StartTime > DateTime.Now)
            .OrderBy(s => s.StartTime)
            .ToList();

        return View(movie);
    }
}
