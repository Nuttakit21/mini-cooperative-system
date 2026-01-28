using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCoop.Api.DTOs;
using MiniCoop.Application.Auth;
using MiniCoop.Infrastructure.Data;

namespace MiniCoop.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly MiniCoopDbContext _db;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(MiniCoopDbContext db, IJwtTokenService jwtTokenService)
    {
        _db = db;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null)
            return Unauthorized("Username หรือ Password ไม่ถูกต้อง");

        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isValid)
            return Unauthorized("Password ไม่ถูกต้อง");

        var token = _jwtTokenService.GenerateToken(user.Id, user.Username, user.Role);

        return Ok(new LoginResponse
        {
            Token = token,
            FullName = user.FullName,
            Role = user.Role
        });
    }
}