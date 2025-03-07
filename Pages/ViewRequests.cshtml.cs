using Hotel_Booking_New.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ViewRequestModel : PageModel
{
    private readonly HotelContext _context;

    public ViewRequestModel(HotelContext context)
    {
        _context = context;
    }

    public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    public async Task OnGetAsync()
    {
        // Fetch only hotels that are pending approval
        Hotels = await _context.Hotels.Where(h => h.Status == "Pending").ToListAsync();
    }

    public async Task<IActionResult> OnPostApproveAsync(int hotelId)
    {
        var hotel = await _context.Hotels.FindAsync(hotelId);
        if (hotel == null)
        {
            return NotFound();
        }

        hotel.Status = "Approved"; // Mark as Approved
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRejectAsync(int hotelId)
    {
        var hotel = await _context.Hotels.FindAsync(hotelId);
        if (hotel == null)
        {
            return NotFound();
        }

        _context.Hotels.Remove(hotel); // Remove from database
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
