using CinemaBookingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Controllers;

public class CinemasController : Controller
{
    private readonly ApplicationDbContext _context;

    public CinemasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET /Cinemas
    public async Task<IActionResult> Index()
    {
        var cinemas = await _context.Cinemas
            .Include(c => c.Halls)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(cinemas);
    }

    // GET /Cinemas/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var cinema = await _context.Cinemas
            .Include(c => c.Halls)
                .ThenInclude(h => h.Showtimes.Where(s => s.StartTime > DateTime.Now))
                    .ThenInclude(s => s.Movie)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cinema is null)
        {
            return NotFound();
        }

        return View(cinema);
    }
}
