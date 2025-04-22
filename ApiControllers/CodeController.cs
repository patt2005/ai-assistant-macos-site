using AIAssistantMacos.Persistance;
using AIAssistantMacos.Persistance.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIAssistantMacos.ApiControllers;

[ApiController]
[Route("/api/code/")]
public class CodeController : ControllerBase
{
    private readonly  AppDbContext _dbContext;

    public CodeController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    private static string GenerateActivationCode()
    {
        const string prefix = "PRO";
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        string part1 = new string(Enumerable.Repeat(chars, 4)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        string part2 = new string(Enumerable.Repeat(chars, 4)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return $"{prefix}-{part1}-{part2}";
    }

    [HttpPost("create-code")]
    public async Task<IActionResult> CreateCode()
    {
        var code = new Code
        {
            Id = Guid.NewGuid(),
            Content = GenerateActivationCode(),
            CreatedAt = DateTime.UtcNow,
            Status = CodeStatus.Active
        };
        
        await _dbContext.Codes.AddAsync(code);
        await _dbContext.SaveChangesAsync();

        return Ok(new { Code = code.Content });
    }

    [HttpPost("activate-code")]
    public async Task<IActionResult> ActivateCode([FromQuery] Guid userId, [FromQuery] string activationCode)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (foundUser == null)
        {
            return NotFound("User not found");
        }
        
        if (foundUser.IsPro)
        {
            return BadRequest("User already has Pro access.");
        }
        
        var foundCode = await _dbContext.Codes.FirstOrDefaultAsync(c => c.Content == activationCode);

        if (foundCode == null)
        {
            return NotFound("Code not found");
        }
        
        if (foundCode.Status == CodeStatus.Used)
        {
            return BadRequest("This code has already been used.");
        }
        
        foundUser.CodeId = foundCode.Id;
        foundUser.IsPro = true;

        foundCode.Status = CodeStatus.Used;
        foundCode.ActivatedAt = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        
        return Ok($"Code {activationCode} was activated");
    }
}