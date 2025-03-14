using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTime.Model;

namespace MyTime.Pages
{
    public class IndexModels(SiteUserContext context) : PageModel
    {
        private readonly SiteUserContext _context = context;

        [BindProperty]
        public SiteUsers SiteUser { get; set; } = new SiteUsers();

        public void OnGet()
        {
            // This handles GET requests.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Automatically populate Created_at with the current time.
            SiteUser.Created_at = DateTime.Now;

            // Add the new user to the database.
            _context.Users.Add(SiteUser);
            await _context.SaveChangesAsync();

            // Redirect or display a success message.
            return RedirectToPage(); // Reloads the page after submission.
        }
    }
    public class AnotherIndexModel(SiteUserContext context) : PageModel
    {
        private readonly SiteUserContext _context = context;

        [BindProperty]
        public SiteUsers SiteUser { get; set; } = new SiteUsers();

        public void OnGet()
        {
            // This handles GET requests.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Automatically populate Created_at with the current time.
            SiteUser.Created_at = DateTime.Now;

            // Add the new user to the database.
            _context.Users.Add(SiteUser);
            await _context.SaveChangesAsync();

            // Redirect or display a success message.
            return RedirectToPage(); // Reloads the page after submission.
        }
    }
}

