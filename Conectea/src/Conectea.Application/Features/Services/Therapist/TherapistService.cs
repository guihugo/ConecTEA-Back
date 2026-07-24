using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;

public class TherapistService : ITherapistService
{
    private readonly ICurrentUser _currentUserService;
    private readonly IPatientRepository _patientRepository;
    private readonly ITherapistRepository _therapistRepository;

    public TherapistService(ICurrentUser currentUser, IPatientRepository patientRepository, ITherapistRepository therapistRepository)
    {
        _currentUserService = currentUser;
        _patientRepository = patientRepository;
        _therapistRepository = therapistRepository;
    }
    public async Task<IEnumerable<PatientResponse>> GetByPacientByTherapistIdAsync()
    {
        Guid userId = _currentUserService.UserId;
        Therapist therapist = await _therapistRepository
            .GetByUserIdAsync(userId) ?? throw new NotFoundException("Terapeuta não encontrado");
            
        IEnumerable<Patient> patients = await _patientRepository.GetByTherapistIdAsync(therapist.Id);
        return patients.Select(p => new PatientResponse
        {
            Id = p.Id,
            FullName = p.FullName,
            BirthDate = p.BirthDate,
            Gender = p.Gender,
            Diagnosis = p.Diagnosis,
            Observation = p.Observation,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
    }
}