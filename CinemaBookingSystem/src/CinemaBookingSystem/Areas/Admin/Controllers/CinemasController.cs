using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CinemasController : Controller
{
    private readonly ApplicationDbContext _context;

    public CinemasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cinemas = await _context.Cinemas.Include(c => c.Halls).OrderBy(c => c.Name).ToListAsync();
        return View(cinemas);
    }

    [HttpGet]
    public IActionResult Create() => View(new Cinema());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cinema cinema)
    {
        if (!ModelState.IsValid) return View(cinema);

        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Cinema created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema is null) return NotFound();
        return View(cinema);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Cinema cinema)
    {
        if (id != cinema.Id) return NotFound();
        if (!ModelState.IsValid) return View(cinema);

        _context.Cinemas.Update(cinema);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Cinema updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var cinema = await _context.Cinemas.Include(c => c.Halls).FirstOrDefaultAsync(c => c.Id == id);
        if (cinema is null)
        {
            TempData["ErrorMessage"] = "Cinema not found.";
            return RedirectToAction(nameof(Index));
        }

        if (cinema.Halls.Any())
        {
            TempData["ErrorMessage"] = "Cannot delete a cinema that still has halls. Remove its halls first.";
            return RedirectToAction(nameof(Index));
        }

        _context.Cinemas.Remove(cinema);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Cinema deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
