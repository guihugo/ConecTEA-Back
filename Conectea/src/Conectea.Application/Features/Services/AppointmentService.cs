using Conectea.Application.DTOs;
using Conectea.Application.DTOs.Patients;
using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;
using Conectea.Domain.Enums;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository appointmentRepository;
    private readonly ICurrentUser _currentUserService;
    private readonly ITherapistRepository _therapistRepository;
    private readonly IGuardianRepository _guardianRepository;
    private readonly IPatientRepository _patientRepository;

    public AppointmentService(IAppointmentRepository repository, ICurrentUser currentUserService, ITherapistRepository therapistRepository, IGuardianRepository guardianRepository, IPatientRepository patientRepository)
    {
        this.appointmentRepository = repository;
        _currentUserService = currentUserService;
        _therapistRepository = therapistRepository;
        _guardianRepository = guardianRepository;
        _patientRepository = patientRepository;
    }


    public async Task<AppointmentResponse> CreateAsync(CreateAppointmentRequest request)
    {
        Guid userId = _currentUserService.UserId;
        Therapist therapist = await _therapistRepository
            .GetByUserIdAsync(userId) ?? throw new NotFoundException("Terapeuta não encontrado.");

        Appointment appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = request.PatientId,
            TherapistId = therapist.Id,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Notes = request.Notes,
            Status = AppointmentStatus.Scheduled,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await appointmentRepository.AddAsync(appointment);
        return MapToResponse(appointment);
    }


    public async Task<AppointmentResponse?> GetByIdAsync(Guid appointmentId)
    {
        Appointment? appointment = await appointmentRepository.GetByIdAsync(appointmentId) ?? throw new Exception("Agendamento não encontrado");

        return MapToResponse(appointment);
    }

    public async Task<IEnumerable<AppointmentResponse>> GetTherapistAppointmentsAsync()
    {
        Guid userId = _currentUserService.UserId;
        Therapist therapist = await _therapistRepository
            .GetByUserIdAsync(userId) ?? throw new NotFoundException("Terapeuta não encontrado.");

        List<Appointment> appointments = await appointmentRepository
            .GetByTherapistIdAsync(therapist.Id);


        return appointments.Select(MapToResponse);
    }


    public async Task<AppointmentResponse?> GetGuardianNextAppointmentAsync()
    {
        Guid userId = _currentUserService.UserId;
        Guardian guardian = await _guardianRepository.GetByUserIdAsync(userId) ?? throw new NotFoundException("Responsável não encontrado");
        Patient patient = await _patientRepository.GetByGuardianIdAsync(guardian.Id) ?? throw new NotFoundException("Paciente não encontrado");

        Appointment appointment = await appointmentRepository
            .GetNextByPatientIdAsync(patient.Id) ?? throw new NotFoundException("Agendamento não encontrado");

        return MapToResponse(appointment);
    }


    public async Task UpdateStatusAsync(Guid appointmentId, UpdateAppointmentStatusRequest request)
    {
        Appointment? appointment = await appointmentRepository
            .GetByIdAsync(appointmentId) ?? throw new NotFoundException("Agendamento não encontrado");
            
        appointment.Status = request.Status;
        appointment.UpdatedAt = DateTime.UtcNow;


        await appointmentRepository.UpdateAsync(appointment);
    }


    public async Task UpdateAsync(
        Guid appointmentId,
        UpdateAppointmentRequest request)
    {
        Appointment appointment = await appointmentRepository
            .GetByIdAsync(appointmentId) ?? throw new NotFoundException("Agendamento não encontrado");

        appointment.StartTime = request.StartTime;
        appointment.EndTime = request.EndTime;
        appointment.Notes = request.Notes;
        appointment.UpdatedAt = DateTime.UtcNow;


        await appointmentRepository.UpdateAsync(appointment);
    }


    private static AppointmentResponse MapToResponse(Appointment appointment)
    {
        return new AppointmentResponse
        {
            Id = appointment.Id,

            Patient = appointment.Patient == null 
                ? null! 
                : new PatientSummaryResponse
                {
                    Id = appointment.Patient.Id,
                    FullName = appointment.Patient.FullName,
                    Diagnosis = appointment.Patient.Diagnosis,
                    Gender = appointment.Patient.Gender,
                    Age = CalculateAge(
                        appointment.Patient.BirthDate)
                },

            Therapist = appointment.Therapist == null ? null! : new TherapistSummaryResponse
                {
                    Id = appointment.Therapist.Id,
                },

            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Status = appointment.Status,
            Notes = appointment.Notes
        };
    }


    private static int CalculateAge(DateOnly birthDate)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
            age--;

        return age;
    }

    public Task DeleteAsync(Guid appointmentId)
    {
        throw new NotImplementedException();
    }
}