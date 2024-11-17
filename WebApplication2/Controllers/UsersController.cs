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

    [HttpPost]
    public async Task<ActionResult<Users>> PutUsers(Users user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Users>> PutUsers(int id, Users user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }
        context.Users.Update(user);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Users>> DeleteUsers(int id)
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
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return NoContent();
    }
}