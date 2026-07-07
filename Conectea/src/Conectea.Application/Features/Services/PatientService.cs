using Conectea.Application.Interfaces;


namespace Conectea.Application.Services;

public class PatientService : IPatientService
{

    public async Task<Guid> CreateAsync(CreatePatientRequest request)
    {
        throw new NotImplementedException();
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