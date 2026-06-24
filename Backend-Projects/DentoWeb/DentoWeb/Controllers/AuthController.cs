using Microsoft.AspNetCore.Mvc;
using DentoWeb.DTO;
using DentoWeb.Services;

namespace DentoWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto request)
    {
        
        if (request.Username == "admin" && request.Password == "123")
        {
            var token = _jwtService.GenerateToken(request.Username);

            return Ok(new
            {
                token = token
            });
        }

        return Unauthorized("Invalid credentials");
    }
}