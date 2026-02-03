using Microsoft.EntityFrameworkCore;
using MiniCoop.Application.DTOs;
using MiniCoop.Application.Interfaces;

public class ApplicationRepository : IApplicationRepository
{
    private readonly MiniCoopDbContext _db;
    public ApplicationRepository(MiniCoopDbContext db) => _db = db;

    public async Task<List<ApplicationDto>> GetActiveAsync() =>
        await _db.Application.Where(x => x.IS_ACTIVE)
            .OrderBy(x => x.ORDER_NO)
            .Select(x => new ApplicationDto
            {
                APPLICATION_ID = x.APPLICATION_ID,
                APPLICATION_CODE = x.APPLICATION_CODE,
                APPLICATION_NAME = x.APPLICATION_NAME,
                ICON = x.ICON
            }).ToListAsync();
}