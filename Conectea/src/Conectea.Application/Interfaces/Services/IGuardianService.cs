public interface IGuardianService
{
    Task<PatientResponse?>GetByPacientByGuardiantIdAsync(); 
}
