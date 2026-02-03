using MiniCoop.Application.DTOs.Menu;

namespace MiniCoop.Application.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<MenuGroupDto>> GetMenuByApplicationAsync(string application);
    }
}
