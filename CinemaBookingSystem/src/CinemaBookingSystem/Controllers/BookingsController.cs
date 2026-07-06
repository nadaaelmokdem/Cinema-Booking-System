using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using CinemaBookingSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Controllers;

[Authorize] // Every action here requires a logged-in user.
public class BookingsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET /Bookings/Create/5  (5 = showtimeId)
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        var showtime = await _context.Showtimes
            .Include(s => s.Movie)
            .Include(s => s.Hall).ThenInclude(h => h!.Cinema)
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (showtime is null)
        {
            return NotFound();
        }

        if (showtime.HasStarted)
        {
            TempData["ErrorMessage"] = "This showtime has already started and can no longer be booked.";
            return RedirectToAction("Details", "Movies", new { id = showtime.MovieId });
        }

        var vm = new BookingCreateViewModel
        {
            ShowtimeId = showtime.Id,
            MovieTitle = showtime.Movie!.Title,
            CinemaName = showtime.Hall!.Cinema!.Name,
            HallName = showtime.Hall.Name,
            StartTime = showtime.StartTime,
            TicketPrice = showtime.TicketPrice,
            SeatsAvailable = showtime.SeatsAvailable,
            NumberOfSeats = 1
        };

        return View(vm);
    }

    // POST /Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookingCreateViewModel model)
    {
        var showtime = await _context.Showtimes
            .Include(s => s.Movie)
            .Include(s => s.Hall).ThenInclude(h => h!.Cinema)
            .Include(s => s.Bookings)
            .FirstOrDefaultAsync(s => s.Id == model.ShowtimeId);

        if (showtime is null)
        {
            return NotFound();
        }

        // Re-populate read-only display fields in case we need to redisplay the form.
        model.MovieTitle = showtime.Movie!.Title;
        model.CinemaName = showtime.Hall!.Cinema!.Name;
        model.HallName = showtime.Hall.Name;
        model.StartTime = showtime.StartTime;
        model.TicketPrice = showtime.TicketPrice;
        model.SeatsAvailable = showtime.SeatsAvailable;

        if (showtime.HasStarted)
        {
            ModelState.AddModelError(string.Empty, "This showtime has already started and can no longer be booked.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Server-side capacity check - never trust the client for this.
        if (model.NumberOfSeats > showtime.SeatsAvailable)
        {
            ModelState.AddModelError(nameof(model.NumberOfSeats),
                $"Only {showtime.SeatsAvailable} seat(s) left for this showtime.");
            return View(model);
        }

        var userId = _userManager.GetUserId(User);
        if (userId is null)
        {
            return Challenge();
        }

        var booking = new Booking
        {
            UserId = userId,
            ShowtimeId = showtime.Id,
            NumberOfSeats = model.NumberOfSeats,
            TotalPrice = model.NumberOfSeats * showtime.TicketPrice,
            BookedAt = DateTime.Now
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Booking confirmed for {model.NumberOfSeats} seat(s) - {showtime.Movie.Title}.";
        return RedirectToAction(nameof(MyBookings));
    }

    // GET /Bookings/MyBookings
    public async Task<IActionResult> MyBookings()
    {
        var userId = _userManager.GetUserId(User);

        var bookings = await _context.Bookings
            .Include(b => b.Showtime).ThenInclude(s => s!.Movie)
            .Include(b => b.Showtime).ThenInclude(s => s!.Hall).ThenInclude(h => h!.Cinema)
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.BookedAt)
            .ToListAsync();

        return View(bookings);
    }

    // POST /Bookings/Cancel/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = _userManager.GetUserId(User);

        var booking = await _context.Bookings
            .Include(b => b.Showtime)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        if (booking is null)
        {
            TempData["ErrorMessage"] = "Booking not found.";
            return RedirectToAction(nameof(MyBookings));
        }

        if (booking.IsCancelled)
        {
            TempData["ErrorMessage"] = "This booking has already been cancelled.";
            return RedirectToAction(nameof(MyBookings));
        }

        if (booking.Showtime is null || booking.Showtime.HasStarted)
        {
            TempData["ErrorMessage"] = "This showtime has already started, so the booking cannot be cancelled.";
            return RedirectToAction(nameof(MyBookings));
        }

        booking.IsCancelled = true;
        booking.CancelledAt = DateTime.Now;
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Your booking has been cancelled.";
        return RedirectToAction(nameof(MyBookings));
    }
}
