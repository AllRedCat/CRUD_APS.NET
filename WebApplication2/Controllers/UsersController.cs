using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext context) : ControllerBase
{
    private string GenerateSha256Hash(string input)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
    
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
        user.PasswordHash = GenerateSha256Hash(user.PasswordHash);
        
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
        
        user.PasswordHash = GenerateSha256Hash(user.PasswordHash);
        
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