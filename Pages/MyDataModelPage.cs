using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MyTime.Model;

namespace MyTime.Pages;
public class MyDataModelPage(SiteUserContext context) : PageModel
{
    private readonly SiteUserContext _context = context;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool HasError { get; set; } = false;

    public void OnGet()
    {
        // Handle GET requests (no action needed for now).
    }

    public void OnPost()
    {
        if (string.IsNullOrEmpty(Name))
        {
            HasError = true;
            return;
        }

        HasError = false;

        // Query the database for a user with the entered name.
        var user = _context.Users.FirstOrDefault(u => u.Name == Name);

        // If a user is found, set their email to display it.
        Email = user?.Email ?? "No email found for this name.";
    }
}