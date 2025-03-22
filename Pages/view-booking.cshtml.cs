using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTime.Model;

namespace MyTime.Pages
{
    public class ViewBookingModel(SiteUserContext context) : PageModel
    {
        private readonly SiteUserContext _context = context;

        [BindProperty(SupportsGet = true)]
        public string? Hash { get; set; } // Hash from the URL

        public new SiteUsers? User { get; set; } // User details

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Hash))
            {
                // No hash provided, display default view
                return Page();
            }

            // Retrieve user based on hash
            User = _context.Users.FirstOrDefault(u => u.User_hash == Hash);

            if (User != null)
            {
                return Page(); // Display user details
            }

            // If hash is invalid, return Page() with User as null
            return Page();
        }
    }
}

