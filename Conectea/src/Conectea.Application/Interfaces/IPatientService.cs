public interface IPatientService
{
    Task<Guid> CreateAsync(CreatePatientRequest request);

    Task<PatientResponse?> GetByIdAsync(Guid id);

    Task<IEnumerable<PatientResponse>> GetAllAsync();

    Task UpdateAsync(Guid id, UpdatePatientRequest request);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<PatientResponse>> GetByTherapistIdAsync(Guid therapistId);
}