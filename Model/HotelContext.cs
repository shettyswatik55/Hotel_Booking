using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_New.Model
{
    public class HotelContext : DbContext
    {
        public DbSet <Booking> Bookings{ get; set; }
        public DbSet <User> Users { get; set; }
        public DbSet <Hotel>Hotels{ get; set; }
        public DbSet <Room> Rooms { get; set; }
        public DbSet <Feedback> Feedbacks{ get; set; }
        public HotelContext(DbContextOptions options) : base(options)
        {

        }
    }
}
