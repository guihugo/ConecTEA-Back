using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;
using Conectea.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class TherapistRepository : ITherapistRepository
{
    private readonly ApplicationDbContext _context;


    public TherapistRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Therapist?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Therapists
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
    public async Task AddAsync(Therapist therapist)
    {
        await _context.Therapists.AddAsync(therapist);
        await _context.SaveChangesAsync();
    }
}