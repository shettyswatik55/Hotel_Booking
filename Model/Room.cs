using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_New.Model
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [Required] public int HotelId { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
    }
}
