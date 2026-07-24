public interface IPatientService
{
    Task<CreatePatientResponse> CreateAsync(CreatePatientRequest request);
    Task<PatientResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<PatientResponse>> GetAllAsync();
    Task UpdateAsync(Guid id, UpdatePatientRequest request);
    Task<List<Appointment>> GetPatientAppointments(Guid id);
    Task DeleteAsync(Guid id);
}