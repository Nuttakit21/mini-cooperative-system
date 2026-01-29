using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MiniCoop.Api.Controllers;

[ApiController]
[Route("api/menus")]
[Authorize]
public class MenuController : ControllerBase
{
    private readonly MiniCoopDbContext _db;

    public MenuController(MiniCoopDbContext db)
    {
        _db = db;
    }

    [HttpGet("by-role")]
    public async Task<IActionResult> GetMenusByRole()
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(role))
            return Unauthorized("ไม่พบสิทธิ์");

        var menus = await _db.Menus.Where(m => m.IsActive && m.MenuRoles.Any(r => r.Role == role)).OrderBy(m => m.OrderNo)
                                   .Select(m => new { m.Id, m.Name, m.Path, m.Icon }).ToListAsync();

        return Ok(menus);
    }
}