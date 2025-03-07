using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Hotel_Booking_New.Model;
using System;
using System.Threading.Tasks;

public class AddHotelModel : PageModel
{
    private readonly HotelContext _context;

    public AddHotelModel(HotelContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Hotel NewHotel { get; set; }
    [BindProperty]
    public IFormFile HotelImage { get; set; }

    public string SuccessMessage { get; set; }
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // Get logged-in user's username from session
        NewHotel = new Hotel
        {
            UserName = HttpContext.Session.GetString("LoggedInUser") ?? "Guest" // Default if session is empty
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Please fill out all required fields correctly.";
            return Page();
        }

        try
        {
            // Save hotel details
            _context.Hotels.Add(NewHotel);
            await _context.SaveChangesAsync();

            SuccessMessage = "Hotel added successfully!";
            return Page();
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occurred while adding the hotel: " + ex.Message;
            return Page();
        }
    }
}
