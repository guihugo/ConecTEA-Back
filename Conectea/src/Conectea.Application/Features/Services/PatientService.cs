using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;


namespace Conectea.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ICurrentUser _currentUserService;
    private readonly ITherapistRepository _therapistRepository;
    private readonly IGuardianInvitationService _guardianInvitationService;
    public PatientService(IPatientRepository patientRepository, ICurrentUser currentUser, ITherapistRepository therapistRepository, IGuardianInvitationService guardianInvitationService)
    {
        _patientRepository = patientRepository;
        _currentUserService = currentUser;
        _therapistRepository = therapistRepository;
        _guardianInvitationService = guardianInvitationService;
    }
    public async Task<CreatePatientResponse> CreateAsync(CreatePatientRequest request)
    {
        var userId = _currentUserService.UserId;


        var therapist = await _therapistRepository
            .GetByUserIdAsync(userId);


        if (therapist is null)
        {
            throw new Exception(
                "Usuário não possui terapeuta associado."
            );
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
            throw new KeyNotFoundException("Paciente não encontrado.");

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

    public async Task<IEnumerable<PatientResponse>> GetByPacientByTherapistIdAsync()
    {
        var userId = _currentUserService.UserId;


        var therapist = await _therapistRepository
            .GetByUserIdAsync(userId);

        if (therapist is null)
        {
            throw new Exception(
                "Terapeuta não encontrado."
            );
        }

        var patients = await _patientRepository.GetByTherapistIdAsync(therapist.Id);
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

    public Task UpdateAsync(Guid id, UpdatePatientRequest request)
    {
        throw new NotImplementedException();
    }
}