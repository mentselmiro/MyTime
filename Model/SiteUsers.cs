using System.ComponentModel.DataAnnotations;

namespace MyTime.Model
{
    public class SiteUsers
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        public DateTime Created_at { get; set; }
    }
}
