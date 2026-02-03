using MiniCoop.Application.DTOs;

namespace MiniCoop.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task<List<ApplicationDto>> GetActiveAsync();
    }
}
