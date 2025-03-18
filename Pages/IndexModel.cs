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

        public void OnPostAsync()
        {
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

        public void OnPostAsync()
        {
        }
    }
}

