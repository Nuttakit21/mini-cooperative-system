using Microsoft.EntityFrameworkCore;
using MiniCoop.Application.DTOs.Menu;
using MiniCoop.Application.Interfaces;

public class MenuRepository : IMenuRepository
{
    private readonly MiniCoopDbContext _db;

    public MenuRepository(MiniCoopDbContext db)
    {
        _db = db;
    }

    public async Task<List<MenuGroupDto>> GetMenuByApplicationAsync(string application)
    {
        return await _db.Application
            .Where(a => a.APPLICATION_CODE == application && a.IS_ACTIVE)
            .SelectMany(a => a.MenuGroups)
            .Where(g => g.IS_ACTIVE)
            .OrderBy(g => g.ORDER_NO)
            .Select(g => new MenuGroupDto
            {
                Id = g.APPLICATION_ID,
                Name = g.APPLICATION_NAME,
                Icon = g.ICON,
                Menus = g.Menus
                    .Where(m => m.IS_ACTIVE)
                    .OrderBy(m => m.ORDER_NO)
                    .Select(m => new MenuItemDto
                    {
                        Id = m.MENU_ID,
                        Name = m.MENU_NAME,
                        Path = m.PATH
                    }).ToList()
            }).ToListAsync();
    }
}
