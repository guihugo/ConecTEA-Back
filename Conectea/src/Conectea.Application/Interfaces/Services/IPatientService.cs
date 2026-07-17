public interface IPatientService
{
    Task<CreatePatientResponse> CreateAsync(CreatePatientRequest request);

    Task<PatientResponse?> GetByIdAsync(Guid id);

    Task<IEnumerable<PatientResponse>> GetAllAsync();

    Task UpdateAsync(Guid id, UpdatePatientRequest request);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<PatientResponse>> GetByPacientByTherapistIdAsync();
    Task<IEnumerable<PatientResponse>> GetByPacientByGuardiantIdAsync();

}