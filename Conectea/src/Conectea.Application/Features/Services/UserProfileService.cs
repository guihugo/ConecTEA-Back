using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;
using Conectea.Domain.Enums;

public class UserProfileService : IUserProfileService
{
    private readonly ITherapistRepository _therapistRepository;
    private readonly IGuardianRepository _guardianRepository;


    public UserProfileService(
        ITherapistRepository therapistRepository,
        IGuardianRepository guardianRepository)
    {
        _therapistRepository = therapistRepository;
        _guardianRepository = guardianRepository;
    }


    public async Task CreateAsync(
        Guid userId,
        UserRole role)
    {
        if (role == UserRole.Therapist)
        {
            await _therapistRepository.AddAsync(
                new Therapist
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                });
        }


        if (role == UserRole.Guardian)
        {
            await _guardianRepository.AddAsync(
                new Guardian
                {
                    Id = Guid.NewGuid(),
                    UserId = userId
                });
        }
    }
}