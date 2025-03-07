using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

public class SignupModel : PageModel
{
    private readonly HotelContext _context;

    public SignupModel(HotelContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string Email { get; set; }

    public string Message { get; set; }

    public IActionResult OnPost()
    {
        // Check if user already exists
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == Username);
        if (existingUser != null)
        {
            Message = "Username already exists!";
            return Page();
        }

        // Save new user
        var newUser = new User { Username = Username, Password = Password, Email = Email };
        _context.Users.Add(newUser);
        _context.SaveChanges();

        return RedirectToPage("/Login"); // Redirect to Login Page
    }
}
