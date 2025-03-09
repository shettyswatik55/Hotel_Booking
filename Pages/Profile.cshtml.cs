using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class ProfileModel : PageModel
{
    private readonly HotelContext _context;

    public ProfileModel(HotelContext context)
    {
        _context = context;
    }

    public User? UserDetails { get; set; }
    public List<Hotel> Hotels { get; set; } = new List<Hotel>();
    public List<Booking> Bookings { get; set; } = new List<Booking>(); // Added for bookings

    public async Task<IActionResult> OnGetAsync()
    {
        string? loggedInUser = HttpContext.Session.GetString("LoggedInUser");

        if (string.IsNullOrEmpty(loggedInUser))
        {
            return RedirectToPage("/Login"); // Redirect if not logged in
        }

        // Fetch user details
        UserDetails = await _context.Users.FirstOrDefaultAsync(u => u.Username == loggedInUser);

        if (UserDetails != null)
        {
            // Fetch hotels owned by the user
            Hotels = await _context.Hotels
                        .Where(h => h.OwnerUserId == UserDetails.UserID.ToString())
                        .ToListAsync();

            // Fetch bookings made by the user
            Bookings = await _context.Bookings
                        .Where(b => b.UserId == UserDetails.UserID)
                        .ToListAsync();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCancelBookingAsync(int bookingId)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);

        if (booking == null)
        {
            TempData["ErrorMessage"] = "Booking not found.";
            return RedirectToPage();
        }

        // Check if the logged-in user is authorized to cancel this booking
        string? loggedInUser = HttpContext.Session.GetString("LoggedInUserId");
        if (booking.UserId.ToString() != loggedInUser)
        {
            TempData["ErrorMessage"] = "You are not authorized to cancel this booking. your id: " + loggedInUser + "B id: "+booking.UserId;
            return RedirectToPage();
        }

        // Remove the booking from the database
        booking.BookingStatus = "Canceled";
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Booking canceled successfully.";
        return RedirectToPage();
    }
}
