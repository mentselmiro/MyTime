using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTime.MailModel;
using MyTime.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace MyTime.Pages
{
    public class BuyTime(SiteUserContext context, IMailService mailService) : PageModel
    {
        private readonly SiteUserContext _context = context;
        private readonly IMailService _mailService = mailService;

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

        [BindProperty]
        public string? Description { get; set; } // Optional field

        public string ConfirmationMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(SelectedDate) && !string.IsNullOrEmpty(SelectedTimeFrame))
            {
                DateTime selectedDateTime;
                try
                {
                    var date = DateTime.Parse(SelectedDate);
                    var timeRange = SelectedTimeFrame.Split('-');
                    var startTime = TimeSpan.Parse(timeRange[0]);
                    selectedDateTime = date.Add(startTime);
                }
                catch
                {
                    ConfirmationMessage = "Invalid date or time frame format.";
                    return Page();
                }

                // Clean up the description field (trim spaces, remove if empty)
                if (!string.IsNullOrWhiteSpace(Description))
                {
                    Description = Description.Trim();
                }
                else
                {
                    Description = string.Empty;
                }

                // Generate a random, URL-safe key
                string userHash = GenerateUrlSafeRandomKey();

                // Save user record
                var siteUser = new SiteUsers
                {
                    Name = Name,
                    Email = Email,
                    Created_at = DateTime.Now,
                    Reserved_time = selectedDateTime,
                    User_text = Description,
                    User_hash = userHash // Save the random key in user_hash column
                };

                _context.Users.Add(siteUser);
                await _context.SaveChangesAsync();


                // Prepare the email content
                var mailRequest = new MailRequest
                {
                    ToEmail = Email, // Send to the customer's email
                    Subject = "Time Purchase Confirmation",
                    Body = $"Dear {Name}, your booking is confirmed. Use this link to view your details: /ViewBooking/{siteUser.User_hash}"
                };
                try
                {
                    await _mailService.SendEmailAsync(mailRequest);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during email sending
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }


                ConfirmationMessage =
                    $"Thank you, {Name}! Your booking is confirmed. Use this link to view your details: /ViewBooking/{siteUser.User_hash}";
            }
            else
            {
                ConfirmationMessage = "Please complete all fields before submitting.";
            }

            return Page();
        }

        // Method to generate a URL-safe random key
        private static string GenerateUrlSafeRandomKey()
        {
            byte[] randomBytes = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert to Base64 and replace unsafe characters
            string base64Key = Convert.ToBase64String(randomBytes)
                                .Replace("/", "_")
                                .Replace("+", "-")
                                .TrimEnd('=');

            return base64Key; // Example: "Pb7d2InvZg"
        }
    }
}