using Microsoft.EntityFrameworkCore;

namespace MyTime.Model
{
    public class ScoolContext(DbContextOptions<ScoolContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
    }
}
