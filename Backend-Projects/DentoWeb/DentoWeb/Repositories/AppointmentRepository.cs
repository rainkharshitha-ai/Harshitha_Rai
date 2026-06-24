using DentoWeb.Data;
using DentoWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DentoWeb.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment?> GetById(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<Appointment> Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment?> Update(int id, Appointment appointment)
        {
            var existing = await _context.Appointments.FindAsync(id);
            if (existing == null) return null;

            existing.PatientId = appointment.PatientId;
            existing.AppointmentDate = appointment.AppointmentDate;
            existing.Reason = appointment.Reason;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _context.Appointments.FindAsync(id);
            if (data == null) return false;

            _context.Appointments.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}