public interface ITherapistService
{
    Task<IEnumerable<PatientResponse>> GetByPacientByTherapistIdAsync();
}