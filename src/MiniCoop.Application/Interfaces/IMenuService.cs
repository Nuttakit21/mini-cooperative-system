using MiniCoop.Application.DTOs.Menu;

namespace MiniCoop.Application.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuGroupDto>> GetByApplicationAsync(string applicationId);
    }
}
