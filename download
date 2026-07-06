using System.Diagnostics;
using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Public landing page: a handful of movies currently showing.
    public async Task<IActionResult> Index()
    {
        var featured = await _context.Movies
            .Include(m => m.Category)
            .Where(m => m.IsShowing)
            .OrderByDescending(m => m.ReleaseDate)
            .Take(6)
            .ToListAsync();

        return View(featured);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? code)
    {
        if (code == 404)
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        Response.StatusCode = 500;
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
