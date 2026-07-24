using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Entities;
using Conectea.Domain.Enums;
using Conectea.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Conectea.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }


    public async Task<Appointment?> GetByIdAsync(Guid appointmentId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.Therapist)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);
    }


    public async Task<List<Appointment>> GetByTherapistIdAsync(Guid therapistId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.TherapistId == therapistId)
            .Include(a => a.Patient)
            .OrderBy(a => a.StartTime)
            .ToListAsync();
    }


    public async Task<Appointment?> GetNextByPatientIdAsync(Guid patientId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(a =>
                a.PatientId == patientId &&
                a.Status == AppointmentStatus.Scheduled &&
                a.StartTime > DateTime.UtcNow)
            .Include(a => a.Therapist)
            .OrderBy(a => a.StartTime)
            .FirstOrDefaultAsync();
    }


    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }
}