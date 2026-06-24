using DentoWeb.DTOs;
using DentoWeb.Models;
using DentoWeb.Repositories;

namespace DentoWeb.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        private static AppointmentDto Map(Appointment x) => new()
        {
            Id = x.Id,
            PatientId = x.PatientId,
            AppointmentDate = x.AppointmentDate,
            Reason = x.Reason
        };

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointments()
        {
            var data = await _repository.GetAll();
            return data.Select(Map);
        }

        public async Task<AppointmentDto?> GetAppointmentById(int id)
        {
            var x = await _repository.GetById(id);
            return x == null ? null : Map(x);
        }

        public async Task<AppointmentDto> CreateAppointment(CreateAppointmentDto dto)
        {
            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason
            };

            var result = await _repository.Add(entity);
            return Map(result);
        }

        public async Task<AppointmentDto?> UpdateAppointment(int id, UpdateAppointmentDto dto)
        {
            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason
            };

            var result = await _repository.Update(id, entity);
            return result == null ? null : Map(result);
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            return await _repository.Delete(id);
        }
    }
}