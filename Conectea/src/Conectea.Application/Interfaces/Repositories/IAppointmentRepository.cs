using Conectea.Domain.Entities;

namespace Conectea.Application.Interfaces.Repositories;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment);

    Task<Appointment?> GetByIdAsync(Guid appointmentId);

    Task<List<Appointment>> GetByTherapistIdAsync(Guid therapistId);

    Task<Appointment?> GetNextByPatientIdAsync(Guid patientId);

    Task UpdateAsync(Appointment appointment);
}