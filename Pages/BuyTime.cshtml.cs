using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTime.Model;
using System.ComponentModel.DataAnnotations;

namespace MyTime.Pages
{
    public class BuyTime(SiteUserContext context) : PageModel
    {
        private readonly SiteUserContext _context = context;

        [BindProperty]
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string SelectedDate { get; set; } = string.Empty;

        [BindProperty]
        public string SelectedTimeFrame { get; set; } = string.Empty;

        public string ConfirmationMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(SelectedDate) && !string.IsNullOrEmpty(SelectedTimeFrame))
            {
                // Parse SelectedDate and combine it with SelectedTimeFrame to create a DateTime object.
                DateTime selectedDateTime;
                try
                {
                    var date = DateTime.Parse(SelectedDate);
                    var timeRange = SelectedTimeFrame.Split('-');
                    var startTime = TimeSpan.Parse(timeRange[0]); // Parse start time (e.g., "08:00")
                    selectedDateTime = date.Add(startTime); // Combine date with start time
                }
                catch
                {
                    ConfirmationMessage = "Invalid date or time frame format.";
                    return Page();
                }

                // Save to database
                var siteUser = new SiteUsers
                {
                    Name = Name,
                    Email = Email,
                    Created_at = DateTime.Now,
                    Reserved_time = selectedDateTime
                };

                _context.Users.Add(siteUser);
                await _context.SaveChangesAsync();

                ConfirmationMessage = $"Thank you, {Name}! You have successfully booked {SelectedTimeFrame} on {SelectedDate}.";
            }
            else
            {
                ConfirmationMessage = "Please complete all fields before submitting.";
            }

            return Page();
        }
    }
}