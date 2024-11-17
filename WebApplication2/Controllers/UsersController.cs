using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
    {
        if (!context.Users.Any())
        {
            return NotFound();
        }
        return await context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Users>> GetUsers(int id)
    {
        if (!context.Users.Any())
        {
            return NotFound();
        }
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
}