using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Hotel_Booking_New.Model;
using System;
using System.IO;
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
    public IFormFile HotelImage { get; set; } // Uploaded image file

    public string SuccessMessage { get; set; }
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // Set default values
        var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
        var OwnerUserId = HttpContext.Session.GetString("LoggedInUserId");
        if (loggedInUser != null)
        {
            NewHotel = new Hotel
            {
                UserName = loggedInUser,
                OwnerUserId = OwnerUserId
                //OwnerUserId = HttpContext.Session.GetInt32("LoggedInUserId").Value
            };
        }
        else
        {
            // Handle the case where the session value is null
            ErrorMessage = "User is not logged in.";
        }
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
            // Handle Image Upload
            if (HotelImage != null && HotelImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploadsFolder); // Ensure folder exists

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(HotelImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await HotelImage.CopyToAsync(stream);
                }

                // Save the image path in database (relative path)
                NewHotel.ImagePath = "/images/" + uniqueFileName;
            }
            else
            {
                // Set a default image if none is uploaded
                NewHotel.ImagePath = "/images/default-hotel.jpg";
            }

            // Save hotel details
            _context.Hotels.Add(NewHotel);
            await _context.SaveChangesAsync();

            SuccessMessage = "Hotel added successfully!";
            return RedirectToPage("/ViewHotels");
        }
        catch (Exception ex)
        {
            ErrorMessage = "An error occurred while adding the hotel: " + ex.Message;
            return Page();
        }
    }
}
