using System;
using System.Threading.Tasks;
using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; // Added for logging

public class BookHotelModel : PageModel
{
    private readonly HotelContext _context;
    private readonly ILogger<BookHotelModel> _logger; // Logging service

    public BookHotelModel(HotelContext context, ILogger<BookHotelModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public Booking Booking { get; set; } = new Booking
    {
        //CheckInDate = DateTime.Now,
        //CheckOutDate = DateTime.Now.AddDays(1),
        BookingStatus = "Pending",
        PaymentStatus = "Negotiable"
    };

    public Hotel Hotel { get; set; }

    public async Task<IActionResult> OnGetAsync(int hotelId, int userId, string username)
    {
        _logger.LogInformation($"Fetching hotel details for ID: {hotelId}");

        if (hotelId <= 0 || userId <= 0)
        {
            _logger.LogError("Invalid hotelId or userId provided.");
            return BadRequest("Invalid hotel or user ID.");
        }

        // Fetch hotel details
        Hotel = await _context.Hotels.FindAsync(hotelId);
        if (Hotel == null)
        {
            _logger.LogWarning($"Hotel with ID {hotelId} not found.");
            return NotFound("Hotel not found.");
        }
        var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
        // Auto-fill Booking details
        Booking.HotelId = hotelId;
        Booking.UserId = userId;
        Booking.UserName = loggedInUser;

        
        if (string.IsNullOrEmpty(loggedInUser))
        {
            _logger.LogWarning("User is not logged in. Redirecting to login page.");
            return RedirectToPage("/Login"); // Redirect if user session is missing
        }

        Booking.UserName = username;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Hotel = await _context.Hotels.FindAsync(Booking.HotelId);
            _logger.LogWarning("Model validation failed. Returning to booking form.");
            return Page();
        }

        try
        {
            _logger.LogInformation($"Testing.");
            Console.WriteLine("Hello");
            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();
            Console.WriteLine("Await");
            _logger.LogInformation($"Booking successfully created for HotelID {Booking.HotelId} and UserID {Booking.UserId}.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while saving booking: {ex.Message}");
            ModelState.AddModelError("", "An error occurred while processing your booking.");
            Hotel = await _context.Hotels.FindAsync(Booking.HotelId);
            return Page();
        }

        return RedirectToPage("/Index"); // Redirect after booking confirmation
    }
}
