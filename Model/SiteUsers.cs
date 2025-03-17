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

        public DateTime Reserved_time { get; set; }

        public string User_text { get; set; } = string.Empty;
        public string User_hash { get; set; } = string.Empty;
    }
}
