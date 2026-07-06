using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Duration is required.")]
    [Range(1, 1000, ErrorMessage = "Duration must be between 1 and 1000 minutes.")]
    [Display(Name = "Duration (minutes)")]
    public int DurationMinutes { get; set; }

    [Required(ErrorMessage = "Release date is required.")]
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    // Relative path under wwwroot, e.g. /images/posters/xyz.jpg
    [Display(Name = "Poster")]
    public string? PosterPath { get; set; }

    public bool IsShowing { get; set; } = true;

    [Required(ErrorMessage = "Please choose a category.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
