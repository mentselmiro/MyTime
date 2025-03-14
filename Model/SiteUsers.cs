namespace MyTime.Model;

public class SiteUsers
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime Created_at { get; set; }
}
