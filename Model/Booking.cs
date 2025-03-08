using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_New.Model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        //[Required]
        //[ForeignKey("Hotel")]
        public int HotelId { get; set; }

        //[Required]
        //[ForeignKey("User")]
        public int UserId { get; set; }

        public string UserName { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Guests must be at least 1.")]
        public int Guests { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string ContactNumber { get; set; } // User contact number

        [StringLength(500)]
        public string? SpecialRequests { get; set; } // Additional requirements (optional)

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; } = 0; // Negotiable, default 0

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; } = "Pending"; // Default: Pending

        [Required]
        [StringLength(20)]
        public string BookingStatus { get; set; } = "Pending"; // Default: Pending

        [Required]
        public DateTime RequestCreatedAt { get; set; } = DateTime.UtcNow; // Default current time

        // Navigation Properties
       
    }
}
