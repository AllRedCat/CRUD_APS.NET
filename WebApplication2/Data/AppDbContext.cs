using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Users> Users { get; set; } = null!;
}