using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using CinemaBookingSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    public MoviesController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.Include(m => m.Category).OrderBy(m => m.Title).ToListAsync();
        return View(movies);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View(await BuildFormAsync(new MovieFormViewModel()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MovieFormViewModel model)
    {
        if (model.PosterFile is not null && !IsValidPoster(model.PosterFile, out var error))
        {
            ModelState.AddModelError(nameof(model.PosterFile), error);
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildFormAsync(model));
        }

        var movie = new Movie
        {
            Title = model.Title,
            Description = model.Description,
            DurationMinutes = model.DurationMinutes,
            ReleaseDate = model.ReleaseDate,
            CategoryId = model.CategoryId,
            IsShowing = model.IsShowing
        };

        if (model.PosterFile is not null)
        {
            movie.PosterPath = await SavePosterAsync(model.PosterFile);
        }

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Movie created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        var model = new MovieFormViewModel
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            CategoryId = movie.CategoryId,
            IsShowing = movie.IsShowing,
            ExistingPosterPath = movie.PosterPath
        };

        return View(await BuildFormAsync(model));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MovieFormViewModel model)
    {
        if (id != model.Id) return NotFound();

        var movie = await _context.Movies.FindAsync(id);
        if (movie is null) return NotFound();

        if (model.PosterFile is not null && !IsValidPoster(model.PosterFile, out var error))
        {
            ModelState.AddModelError(nameof(model.PosterFile), error);
        }

        if (!ModelState.IsValid)
        {
            model.ExistingPosterPath = movie.PosterPath;
            return View(await BuildFormAsync(model));
        }

        movie.Title = model.Title;
        movie.Description = model.Description;
        movie.DurationMinutes = model.DurationMinutes;
        movie.ReleaseDate = model.ReleaseDate;
        movie.CategoryId = model.CategoryId;
        movie.IsShowing = model.IsShowing;

        if (model.PosterFile is not null)
        {
            DeletePosterFile(movie.PosterPath);
            movie.PosterPath = await SavePosterAsync(model.PosterFile);
        }

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Movie updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _context.Movies.Include(m => m.Showtimes).FirstOrDefaultAsync(m => m.Id == id);
        if (movie is null)
        {
            TempData["ErrorMessage"] = "Movie not found.";
            return RedirectToAction(nameof(Index));
        }

        if (movie.Showtimes.Any())
        {
            TempData["ErrorMessage"] = "Cannot delete a movie that has showtimes. Remove its showtimes first.";
            return RedirectToAction(nameof(Index));
        }

        DeletePosterFile(movie.PosterPath);
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Movie deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<MovieFormViewModel> BuildFormAsync(MovieFormViewModel model)
    {
        model.Categories = await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new SelectOption { Value = c.Id.ToString(), Text = c.Name })
            .ToListAsync();
        return model;
    }

    private static bool IsValidPoster(Microsoft.AspNetCore.Http.IFormFile file, out string error)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
        {
            error = "Poster must be a .jpg, .png or .webp image.";
            return false;
        }

        if (file.Length > MaxFileSizeBytes)
        {
            error = "Poster image must be smaller than 5 MB.";
            return false;
        }

        error = string.Empty;
        return true;
    }

    private async Task<string> SavePosterAsync(Microsoft.AspNetCore.Http.IFormFile file)
    {
        var postersFolder = Path.Combine(_environment.WebRootPath, "images", "posters");
        Directory.CreateDirectory(postersFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(postersFolder, fileName);

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"/images/posters/{fileName}";
    }

    private void DeletePosterFile(string? posterPath)
    {
        if (string.IsNullOrWhiteSpace(posterPath)) return;

        var fullPath = Path.Combine(_environment.WebRootPath, posterPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }
    }
}
