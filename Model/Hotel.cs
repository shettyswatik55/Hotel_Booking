using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_New.Model
{
    public class Hotel
    {
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Hotel Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string? ImagePath { get; set; } // ✅ Allow null values // Path to store uploaded image

        [Required]
        public string Status { get; set; } = "Pending"; // Default status is 'Pending'
    }
}
