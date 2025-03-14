using Microsoft.EntityFrameworkCore;

namespace MyTime.Model;

public class SiteUserContext(DbContextOptions<SiteUserContext> option) : DbContext(option)
{
    public DbSet<SiteUsers> Users { get; set; }
}
