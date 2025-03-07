using System.ComponentModel.DataAnnotations;

namespace Hotel_Booking_New.Model
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [Required] public int UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
