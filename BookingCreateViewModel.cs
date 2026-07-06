using System.ComponentModel.DataAnnotations;

namespace CinemaBookingSystem.Models;

public class Cinema
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cinema name is required.")]
    [StringLength(150, ErrorMessage = "Name cannot be longer than 150 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(300, ErrorMessage = "Address cannot be longer than 300 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required.")]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    public ICollection<Hall> Halls { get; set; } = new List<Hall>();
}
