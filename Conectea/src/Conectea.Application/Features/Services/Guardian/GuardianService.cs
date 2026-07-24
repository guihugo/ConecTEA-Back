
using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;

public class GuardianService : IGuardianService
{   
    private readonly ICurrentUser currentUserService;
    private readonly IGuardianRepository guardianRepository;
    private readonly IPatientRepository patientRepository;
    public GuardianService(ICurrentUser currentUserService, IGuardianRepository guardianRepository, IPatientRepository patientRepository)
    {
        this.currentUserService = currentUserService;
        this.guardianRepository = guardianRepository;
        this.patientRepository = patientRepository;
    }

    public async Task<PatientResponse?> GetByPacientByGuardiantIdAsync()
    {
        Guid userId = this.currentUserService.UserId;
        Guardian guardian = await this.guardianRepository.GetByUserIdAsync(userId) ?? throw new NotFoundException("Responsável não encontrado.");

        Patient patient = await this.patientRepository.GetByIdAsync(guardian.Id)
            ?? throw new NotFoundException("Paciente não encontrado.");

        return new PatientResponse
        {
            Id = patient.Id,
            FullName = patient.FullName,
            BirthDate = patient.BirthDate,
            Gender = patient.Gender,
            Diagnosis = patient.Diagnosis,
            Observation = patient.Observation,
            CreatedAt = patient.CreatedAt,
            UpdatedAt = patient.UpdatedAt
        };
    }
}