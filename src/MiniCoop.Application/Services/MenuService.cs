using MiniCoop.Application.DTOs.Menu;
using MiniCoop.Application.Interfaces;

namespace MiniCoop.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repo;

        public MenuService(IMenuRepository repo)
        {
            _repo = repo;
        }

        public Task<List<MenuGroupDto>> GetByApplicationAsync(string application) => _repo.GetMenuByApplicationAsync(application);
    }
}