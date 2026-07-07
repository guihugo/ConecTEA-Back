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
    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PatientResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PatientResponse?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, UpdatePatientRequest request)
    {
        throw new NotImplementedException();
    }
}