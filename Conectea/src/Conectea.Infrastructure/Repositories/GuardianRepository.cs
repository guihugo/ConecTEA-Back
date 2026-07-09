using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;
using Conectea.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Conectea.Infrastructure.Persistence.Repositories;

public class GuardianRepository : IGuardianRepository
{
    private readonly ApplicationDbContext _context;


    public GuardianRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Guardian?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Guardians
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }


    public async Task<Guardian?> GetByIdAsync(Guid id)
    {
        return await _context.Guardians
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task AddAsync(Guardian guardian)
    {
        await _context.Guardians.AddAsync(guardian);
        await _context.SaveChangesAsync();
    }
}