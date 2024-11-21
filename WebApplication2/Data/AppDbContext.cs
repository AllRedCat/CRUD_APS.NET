using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Users> Users { get; set; } = null!;
}