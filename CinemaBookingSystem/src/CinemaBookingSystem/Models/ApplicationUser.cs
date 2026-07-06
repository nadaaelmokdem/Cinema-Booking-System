using Microsoft.AspNetCore.Identity;

namespace CinemaBookingSystem.Models;

// Extends the built-in Identity user with a couple of extra profile fields.
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
