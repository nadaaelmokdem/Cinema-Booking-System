using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HallsController : Controller
{
    private readonly ApplicationDbContext _context;

    public HallsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var halls = await _context.Halls.Include(h => h.Cinema).OrderBy(h => h.Cinema!.Name).ThenBy(h => h.Name).ToListAsync();
        return View(halls);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateCinemasAsync();
        return View(new Hall());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Hall hall)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCinemasAsync();
            return View(hall);
        }

        _context.Halls.Add(hall);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Hall created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var hall = await _context.Halls.FindAsync(id);
        if (hall is null) return NotFound();
        await PopulateCinemasAsync();
        return View(hall);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Hall hall)
    {
        if (id != hall.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            await PopulateCinemasAsync();
            return View(hall);
        }

        _context.Halls.Update(hall);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Hall updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var hall = await _context.Halls.Include(h => h.Showtimes).FirstOrDefaultAsync(h => h.Id == id);
        if (hall is null)
        {
            TempData["ErrorMessage"] = "Hall not found.";
            return RedirectToAction(nameof(Index));
        }

        if (hall.Showtimes.Any())
        {
            TempData["ErrorMessage"] = "Cannot delete a hall that has showtimes. Remove its showtimes first.";
            return RedirectToAction(nameof(Index));
        }

        _context.Halls.Remove(hall);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Hall deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCinemasAsync()
    {
        ViewBag.Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync();
    }
}
