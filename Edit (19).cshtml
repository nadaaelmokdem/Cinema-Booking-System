using CinemaBookingSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BookingsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Admin can see every booking across the whole system.
    public async Task<IActionResult> Index()
    {
        var bookings = await _context.Bookings
            .Include(b => b.User)
            .Include(b => b.Showtime).ThenInclude(s => s!.Movie)
            .Include(b => b.Showtime).ThenInclude(s => s!.Hall).ThenInclude(h => h!.Cinema)
            .OrderByDescending(b => b.BookedAt)
            .ToListAsync();

        return View(bookings);
    }
}
