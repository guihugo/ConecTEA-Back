using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;


namespace Conectea.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<Guid> CreateAsync(CreatePatientRequest request)
    {
        var patient = new Patient
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

        patient.Guardians.Add(new PatientGuardian
        {
            PatientId = patient.Id,
            GuardianId = request.GuardianId,
            Relationship = request.GuardianRelationship
        });

        if (request.TherapistId.HasValue)
        {
            patient.Therapists.Add(new PatientTherapist
            {
                PatientId = patient.Id,
                TherapistId = request.TherapistId.Value,
                StartDate = DateTime.UtcNow,
                IsMainTherapist = true
            });
        }

        await _patientRepository.AddAsync(patient);

        return patient.Id;
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

    public async Task<IEnumerable<PatientResponse>> GetByTherapistIdAsync(Guid therapistId)
    {
        var patients = await _patientRepository.GetByTherapistIdAsync(therapistId);
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