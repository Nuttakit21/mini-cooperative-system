using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCoop.Application.Interfaces;

namespace MiniCoop.Api.Controllers;

[ApiController]
[Route("api/menu")]
[Authorize]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet("{appCode}")]
    public async Task<IActionResult> GetMenu(string appCode)
    {
        var result = await _menuService.GetByApplicationAsync(appCode);
        return Ok(result);
    }
}