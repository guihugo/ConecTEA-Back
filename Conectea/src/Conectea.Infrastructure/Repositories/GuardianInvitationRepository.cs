using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conectea.Infrastructure.Persistence.Repositories;

public class GuardianInvitationRepository : IGuardianInvitationRepository
{
    private readonly ApplicationDbContext _context;

    public GuardianInvitationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(GuardianInvitation invitation)
    {
        await _context.GuardianInvitations.AddAsync(invitation);
        await _context.SaveChangesAsync();
    }

    public async Task<GuardianInvitation?> GetByTokenAsync(string token)
    {
        return await _context.GuardianInvitations
            .Include(x => x.Patient)
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task UpdateAsync(GuardianInvitation invitation)
    {
        _context.GuardianInvitations.Update(invitation);
        await _context.SaveChangesAsync();
    }
}