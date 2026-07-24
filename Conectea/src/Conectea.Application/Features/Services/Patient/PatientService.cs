using System.Runtime.InteropServices;
using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;


namespace Conectea.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ICurrentUser _currentUserService;
    private readonly ITherapistRepository _therapistRepository;
    private readonly IGuardianRepository _guardianRepository;
    private readonly IGuardianInvitationService _guardianInvitationService;
    private readonly IAppointmentRepository appointmentRepository;
    public PatientService(IPatientRepository patientRepository, ICurrentUser currentUser, ITherapistRepository therapistRepository,
    IGuardianRepository guardianRepository, IGuardianInvitationService guardianInvitationService, IAppointmentRepository appointmentRepository)
    {
        _patientRepository = patientRepository;
        _currentUserService = currentUser;
        _therapistRepository = therapistRepository;
        _guardianRepository = guardianRepository;
        _guardianInvitationService = guardianInvitationService;
        this.appointmentRepository = appointmentRepository;
    }
    public async Task<CreatePatientResponse> CreateAsync(CreatePatientRequest request)
    {
        var userId = _currentUserService.UserId;


        var therapist = await _therapistRepository
            .GetByUserIdAsync(userId);


        if (therapist is null)
        {
            throw new NotFoundException("Terapeuta não encontrado.");
        }


        Patient patient = new Patient
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


        patient.Therapists.Add(new PatientTherapist
        {
            PatientId = patient.Id,
            TherapistId = therapist.Id,
            StartDate = DateTime.UtcNow,
            IsMainTherapist = true
        });


        await _patientRepository.AddAsync(patient);

        string invitationCode = await _guardianInvitationService.CreateAsync(patient.Id);

        return new CreatePatientResponse
        {
            PatientId = patient.Id,
            InvitationCode = invitationCode
        };
    }
    public async Task DeleteAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);

        if (patient is null)
            throw new NotFoundException("Paciente não encontrado.");

        await _patientRepository.DeleteAsync(patient);
    }

    public Task<IEnumerable<PatientResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PatientResponse?> GetByIdAsync(Guid id)
    {
        Patient patient = await _patientRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Paciente não encontrado.");

        return new PatientResponse
        {
            Id = patient.Id,
            FullName = patient.FullName,
            BirthDate = patient.BirthDate,
            Gender = patient.Gender,
            Diagnosis = patient.Diagnosis,
            Observation = patient.Observation,
            CreatedAt = patient.CreatedAt,
            UpdatedAt = patient.UpdatedAt
        };
    }

    public async Task<List<Appointment>> GetPatientAppointments(Guid patientId)
    {
        return await appointmentRepository.GetAppointmentsByPatientIdAsync(patientId)
            ?? throw new Exception("Nenhum agendamento encontrado");
    }
    public Task UpdateAsync(Guid id, UpdatePatientRequest request)
    {
        throw new NotImplementedException();
    }
}