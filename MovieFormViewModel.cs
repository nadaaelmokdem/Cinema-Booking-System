using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models;

// e.g. Action, Drama, Comedy, Animation ...
public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters.")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
