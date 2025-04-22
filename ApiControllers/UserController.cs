using AIAssistantMacos.Persistance;
using AIAssistantMacos.Persistance.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIAssistantMacos.ApiControllers;

[ApiController]
[Route("/api/user/")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("get-api-key")]
    public async Task<IActionResult> GetApiKey()
    {
        var apiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");
        
        return Ok(apiKey);
    }

    [HttpGet("fetch-user")]
    public async Task<IActionResult> FetchUser([FromQuery] Guid userId)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (foundUser == null)
        {
            return NotFound("User not found");
        }

        return Ok(foundUser);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromQuery] Guid userId)
    {
        var user = new User
        {
            Id = userId,
            IsPro = false,
            RegisterDate = DateTime.UtcNow
        };
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        
        return Ok("User was successfully registered");
    }
}