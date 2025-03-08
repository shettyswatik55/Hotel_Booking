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

    public async Task<IActionResult> OnGetAsync()
    {
        string? loggedInUser = HttpContext.Session.GetString("LoggedInUser");

        if (string.IsNullOrEmpty(loggedInUser))
        {
            return RedirectToPage("/Login"); // Redirect if not logged in
        }

        UserDetails = await _context.Users.FirstOrDefaultAsync(u => u.Username == loggedInUser);

        if (UserDetails != null)
        {
            Hotels = await _context.Hotels
                        .Where(h => h.UserName == UserDetails.Username)
                        .ToListAsync();
        }

        return Page();
    }
}
