using Microsoft.AspNetCore.Mvc;

namespace RazorPagesUI.Api.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static readonly Dictionary<string, (string Password, string Role)> Users = new()
    {
        ["user"] = ("123", "User"),
        ["admin"] = ("123", "Admin")
    };

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest? req)
    {
        if (req is null || string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest();

        if (!Users.TryGetValue(req.Username, out var data) || data.Password != req.Password)
            return Unauthorized();

        var token = JwtTokenFactory.Create(req.Username, data.Role); // см. ниже
        return Ok(new { accessToken = token, tokenType = "Bearer" });
    }
}

public record LoginRequest(string Username, string Password);