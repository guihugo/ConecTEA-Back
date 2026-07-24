using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;


namespace Conectea.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ICurrentUser _currentUserService;
    private readonly ITherapistRepository _therapistRepository;
    private readonly IGuardianRepository _guardianRepository;
    private readonly IGuardianInvitationService _guardianInvitationService;
    public PatientService(IPatientRepository patientRepository, ICurrentUser currentUser, ITherapistRepository therapistRepository,
    IGuardianRepository guardianRepository, IGuardianInvitationService guardianInvitationService)
    {
        _patientRepository = patientRepository;
        _currentUserService = currentUser;
        _therapistRepository = therapistRepository;
        _guardianRepository = guardianRepository;
        _guardianInvitationService = guardianInvitationService;
    }
    public async Task<CreatePatientResponse> CreateAsync(CreatePatientRequest request)
    {
        var userId = _currentUserService.UserId;


        var therapist = await _therapistRepository
            .GetByUserIdAsync(userId);


        if (therapist is null)
        {
            throw new NotFoundException("Terapeuta não encontrado.");
        }


        Patient patient = new Patient
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            Diagnosis = request.Diagnosis,
            Observation = request.Observation,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };


        patient.Therapists.Add(new PatientTherapist
        {
            PatientId = patient.Id,
            TherapistId = therapist.Id,
            StartDate = DateTime.UtcNow,
            IsMainTherapist = true
        });


        await _patientRepository.AddAsync(patient);

        string invitationCode = await _guardianInvitationService.CreateAsync(patient.Id);

        return new CreatePatientResponse
        {
            PatientId = patient.Id,
            InvitationCode = invitationCode
        };
    }
    public async Task DeleteAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);

        if (patient is null)
            throw new NotFoundException("Paciente não encontrado.");

        await _patientRepository.DeleteAsync(patient);
    }

    public Task<IEnumerable<PatientResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PatientResponse?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PatientResponse> GetMyPatientAsync()
    {
        var userId = _currentUserService.UserId;

        var guardian = await _guardianRepository.GetByUserIdAsync(userId)
            ?? throw new NotFoundException("Responsável não encontrado.");

        var patient = await _patientRepository.GetByGuardianIdAsync(guardian.Id)
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

    public Task UpdateAsync(Guid id, UpdatePatientRequest request)
    {
        throw new NotImplementedException();
    }
}