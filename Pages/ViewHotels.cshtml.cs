using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hotel_Booking_New.Model;

public class ViewHotelsModel : PageModel
{
    private readonly HotelContext _context;

    public ViewHotelsModel(HotelContext context)
    {
        _context = context;
    }

    public List<Hotel> Hotels { get; set; }
    public Dictionary<int, List<Feedback>> Feedbacks { get; set; } = new Dictionary<int, List<Feedback>>();

    [BindProperty]
    public Feedback NewFeedback { get; set; }

    public int LoggedInUserId { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        // Get approved hotels
        Hotels = await _context.Hotels.Where(h => h.Status == "Approved").ToListAsync();

        // Load feedback for each hotel
        foreach (var hotel in Hotels)
        {
            var feedbacks = await _context.Feedbacks.Where(f => f.HotelId == hotel.HotelId).ToListAsync();
            Feedbacks[hotel.HotelId] = feedbacks;
        }

        // Mock user session (replace with real session management)
        LoggedInUserId = 1; // Replace with HttpContext.Session.GetInt32("UserId") if using sessions.

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return await OnGetAsync();
        }

        // Save feedback to database
        _context.Feedbacks.Add(NewFeedback);
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
