using MiniCoop.Application.DTOs;
using MiniCoop.Application.Interfaces;

namespace MiniCoop.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _repo;

        public ApplicationService(IApplicationRepository repo)
        {
            _repo = repo;
        }

        public Task<List<ApplicationDto>> GetActiveAsync() => _repo.GetActiveAsync();
    }
}