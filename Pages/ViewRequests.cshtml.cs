using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using YourNamespace.Data; // Update with your actual namespace
//using YourNamespace.Models; // Update with your actual namespace

public class ViewRequestModel : PageModel
{
    private readonly HotelContext _context; // Inject DB context

    public ViewRequestModel(HotelContext context)
    {
        _context = context;
    }

    public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    public async Task OnGetAsync()
    {
        Hotels = await _context.Hotels.ToListAsync(); // Fetch all hotels
    }
}
