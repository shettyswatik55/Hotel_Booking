using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel(HotelContext context) : PageModel
{
    private readonly HotelContext _context = context;

    public required List<Hotel> Hotels { get; set; }

    public void OnGet()
    {
        Hotels = _context.Hotels.ToList(); // Read all hotels
    }

    public IActionResult OnPostCreate(int HotelId, string Name, string location)
    {
        var hotel = new Hotel { Name = Name, City = location};
        _context.Hotels.Add(hotel);
        _context.SaveChanges();
        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        var hotel = _context.Hotels.Find(id);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }
        return RedirectToPage();
    }
}
