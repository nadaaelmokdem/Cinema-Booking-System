using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBookingSystem.Models;

public class Showtime
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please choose a movie.")]
    [Display(Name = "Movie")]
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }

    [Required(ErrorMessage = "Please choose a hall.")]
    [Display(Name = "Hall")]
    public int HallId { get; set; }
    public Hall? Hall { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    [Display(Name = "Start Time")]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "Ticket price is required.")]
    [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0.")]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Ticket Price")]
    public decimal TicketPrice { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    // Convenience, not mapped: seats already booked (active, non-cancelled).
    [NotMapped]
    public int SeatsBooked => Bookings?.Where(b => !b.IsCancelled).Sum(b => b.NumberOfSeats) ?? 0;

    [NotMapped]
    public int SeatsAvailable => (Hall?.Capacity ?? 0) - SeatsBooked;

    [NotMapped]
    public bool HasStarted => DateTime.Now >= StartTime;
}
