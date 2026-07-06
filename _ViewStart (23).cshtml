using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ShowtimesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ShowtimesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var showtimes = await _context.Showtimes
            .Include(s => s.Movie)
            .Include(s => s.Hall).ThenInclude(h => h!.Cinema)
            .Include(s => s.Bookings)
            .OrderByDescending(s => s.StartTime)
            .ToListAsync();

        return View(showtimes);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDropdownsAsync();
        return View(new Showtime { StartTime = DateTime.Now.AddHours(1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Showtime showtime)
    {
        await ValidateShowtimeAsync(showtime);

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(showtime);
        }

        _context.Showtimes.Add(showtime);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Showtime created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var showtime = await _context.Showtimes.FindAsync(id);
        if (showtime is null) return NotFound();
        await PopulateDropdownsAsync();
        return View(showtime);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Showtime showtime)
    {
        if (id != showtime.Id) return NotFound();

        await ValidateShowtimeAsync(showtime, id);

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return View(showtime);
        }

        _context.Showtimes.Update(showtime);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Showtime updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var showtime = await _context.Showtimes.Include(s => s.Bookings).FirstOrDefaultAsync(s => s.Id == id);
        if (showtime is null)
        {
            TempData["ErrorMessage"] = "Showtime not found.";
            return RedirectToAction(nameof(Index));
        }

        if (showtime.Bookings.Any(b => !b.IsCancelled))
        {
            TempData["ErrorMessage"] = "Cannot delete a showtime that already has active bookings.";
            return RedirectToAction(nameof(Index));
        }

        _context.Showtimes.Remove(showtime);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Showtime deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    // Prevents two showtimes from overlapping in the same hall.
    private async Task ValidateShowtimeAsync(Showtime showtime, int? excludingId = null)
    {
        var movie = await _context.Movies.FindAsync(showtime.MovieId);
        var hall = await _context.Halls.FindAsync(showtime.HallId);

        if (movie is null)
        {
            ModelState.AddModelError(nameof(showtime.MovieId), "Selected movie does not exist.");
            return;
        }

        if (hall is null)
        {
            ModelState.AddModelError(nameof(showtime.HallId), "Selected hall does not exist.");
            return;
        }

        var newEnd = showtime.StartTime.AddMinutes(movie.DurationMinutes);

        var overlaps = await _context.Showtimes
            .Where(s => s.HallId == showtime.HallId && s.Id != (excludingId ?? 0))
            .AnyAsync(s => showtime.StartTime < s.StartTime.AddMinutes(movie.DurationMinutes) && s.StartTime < newEnd);

        if (overlaps)
        {
            ModelState.AddModelError(nameof(showtime.StartTime), "This hall already has a showtime that overlaps with the selected time.");
        }
    }

    private async Task PopulateDropdownsAsync()
    {
        ViewBag.Movies = await _context.Movies.OrderBy(m => m.Title).ToListAsync();
        ViewBag.Halls = await _context.Halls.Include(h => h.Cinema).OrderBy(h => h.Cinema!.Name).ThenBy(h => h.Name).ToListAsync();
    }
}
