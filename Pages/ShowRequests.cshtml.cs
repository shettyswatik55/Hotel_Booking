using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class ShowRequestsModel : PageModel
{
    private readonly HotelContext _context;

    public ShowRequestsModel(HotelContext context)
    {
        _context = context;
    }

    public List<Booking> Bookings { get; set; } = new List<Booking>();

    [BindProperty(SupportsGet = true)]
    public string? StatusFilter { get; set; } // For filtering requests

    public async Task<IActionResult> OnGetAsync()
    {
        string? loggedInUser = HttpContext.Session.GetString("LoggedInUserId");

        if (string.IsNullOrEmpty(loggedInUser))
        {
            return RedirectToPage("/Login"); // Redirect if not logged in
        }

        // Get bookings for the hotels owned by the logged-in user
        var query = _context.Bookings
                            .Where(b => b.OwnerUserId == loggedInUser)
                            .OrderByDescending(b => b.RequestCreatedAt)
                            .AsQueryable();

        // Apply filter if selected
        if (!string.IsNullOrEmpty(StatusFilter) && StatusFilter != "All")
        {
            query = query.Where(b => b.BookingStatus == StatusFilter);
        }

        Bookings = await query.ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(int bookingId, string newStatus)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);

        if (booking == null)
        {
            TempData["ErrorMessage"] = "Booking not found.";
            return RedirectToPage();
        }

        // Ensure only the owner can modify the booking
        string? loggedInUser = HttpContext.Session.GetString("LoggedInUserId");
        if (booking.OwnerUserId != loggedInUser)
        {
            TempData["ErrorMessage"] = "Unauthorized action.";
            return RedirectToPage();
        }

        // Update the booking status
        booking.BookingStatus = newStatus;
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Booking status updated to {newStatus}.";
        return RedirectToPage();
    }
}
