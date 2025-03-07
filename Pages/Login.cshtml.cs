using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Http; // Required for session
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

public class LoginModel : PageModel
{
    private readonly HotelContext _context;

    public LoginModel(HotelContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public IActionResult OnPost()
    {
        // Authenticate user using only Username & Password
        var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

        if (user != null)
        {
            // Store logged-in user's username in session
            HttpContext.Session.SetString("LoggedInUser", Username);

            return RedirectToPage("/Index"); // Redirect to homepage on successful login
        }

        ErrorMessage = "Invalid username or password!";
        return Page();
    }
}
