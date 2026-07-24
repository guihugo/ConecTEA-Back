using Conectea.Application.DTOs;

namespace Conectea.Application.Interfaces;

public interface IAppointmentService
{
    /// <summary>
    /// Cria um novo agendamento para um paciente.
    /// O TherapistId deve vir do usuário autenticado.
    /// </summary>
    Task<AppointmentResponse> CreateAsync(CreateAppointmentRequest request);


    /// <summary>
    /// Retorna todos os agendamentos do terapeuta autenticado.
    /// </summary>
    Task<IEnumerable<AppointmentResponse>> GetTherapistAppointmentsAsync();


    /// <summary>
    /// Retorna a próxima sessão do paciente vinculado ao guardião autenticado.
    /// </summary>
    Task<AppointmentResponse?> GetGuardianNextAppointmentAsync();


    /// <summary>
    /// Retorna um agendamento específico.
    /// </summary>
    Task<AppointmentResponse?> GetByIdAsync(Guid appointmentId);


    /// <summary>
    /// Atualiza o status de uma sessão.
    /// Ex: Scheduled -> Completed / Cancelled
    /// </summary>
    Task UpdateStatusAsync(
        Guid appointmentId,
        UpdateAppointmentStatusRequest request
    );


    /// <summary>
    /// Atualiza informações do agendamento.
    /// Ex: horário, observações.
    /// </summary>
    Task UpdateAsync(
        Guid appointmentId,
        UpdateAppointmentRequest request
    );


    /// <summary>
    /// Remove/cancela um agendamento.
    /// </summary>
    Task DeleteAsync(Guid appointmentId);
}