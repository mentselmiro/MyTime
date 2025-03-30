using System.ComponentModel.DataAnnotations;

namespace MyTime.Model;


public class EmailStats
{
    [Key]
    public int Id { get; set; }

    public DateTime Date { get; set; } // Date of the first email sent for the day
    public DateTime LastSent { get; set; } // Time of the last email sent
    public int SentEmails { get; set; } // Count of emails sent for the current day

}
