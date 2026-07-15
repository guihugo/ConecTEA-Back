using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;
using Conectea.Domain.Enums;


namespace Conectea.Application.Services;

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
        switch (role)
        {
            case UserRole.Therapist:

                var therapist = await _therapistRepository
                    .GetByUserIdAsync(userId);

                if (therapist != null)
                {
                    throw new ConflictException(
                        "Usuário já possui perfil de terapeuta.");
                }


                await _therapistRepository.AddAsync(
                    new Therapist
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId
                    });

                break;



            case UserRole.Guardian:

                var guardian = await _guardianRepository
                    .GetByUserIdAsync(userId);

                if (guardian != null)
                {
                    throw new ConflictException(
                        "Usuário já possui perfil de responsável.");
                }


                await _guardianRepository.AddAsync(
                    new Guardian
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId
                    });

                break;



            default:

                throw new ValidationException(
                    "Tipo de usuário inválido.");
        }
    }
}