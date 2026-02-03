using MiniCoop.Application.DTOs;

namespace MiniCoop.Application.Interfaces
{
    public interface IApplicationService
    {
        Task<List<ApplicationDto>> GetActiveAsync();
    }
}
