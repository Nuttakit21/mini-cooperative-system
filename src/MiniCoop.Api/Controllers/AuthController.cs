using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCoop.Application.Auth;
using MiniCoop.Application.DTOs.Login;

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
        var user = await _db.USERS
            .FirstOrDefaultAsync(u => u.USERNAME == request.Username);

        if (user == null)
            return Unauthorized("Username หรือ Password ไม่ถูกต้อง");

        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PASSWORD_HASH);
        if (!isValid)
            return Unauthorized("Password ไม่ถูกต้อง");

        var token = _jwtTokenService.GenerateToken(user.ID, user.USERNAME, user.ROLE);

        return Ok(new LoginResponse
        {
            Token = token,
            FullName = user.FULL_NAME,
            Role = user.ROLE
        });
    }
}