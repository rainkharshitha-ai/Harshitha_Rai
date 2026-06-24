using DentoWeb.DTOs;
using DentoWeb.Models;
using DentoWeb.Repositories;
using System.Linq;

namespace DentoWeb.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        // ==========================
        // Mapping Method (Clean Code)
        // ==========================
        private static AppointmentDto MapToDto(Appointment x)
        {
            return new AppointmentDto
            {
                Id = x.Id,
                PatientId = x.PatientId,
                AppointmentDate = x.AppointmentDate,
                Reason = x.Reason
            };
        }

        // ==========================
        // Get All
        // ==========================
        public async Task<IEnumerable<AppointmentDto>> GetAllAppointments()
        {
            var data = await _repository.GetAll();

            if (data == null || !data.Any())
                return new List<AppointmentDto>();

            return data.Select(MapToDto);
        }

        // ==========================
        // Get By Id
        // ==========================
        public async Task<AppointmentDto?> GetAppointmentById(int id)
        {
            if (id <= 0) return null;

            var entity = await _repository.GetById(id);
            if (entity == null) return null;

            return MapToDto(entity);
        }

        // ==========================
        // Create
        // ==========================
        public async Task<AppointmentDto> CreateAppointment(CreateAppointmentDto dto)
        {
            // 🔥 Validation
            if (dto.PatientId <= 0)
                throw new ArgumentException("Invalid PatientId");

            if (dto.AppointmentDate < DateTime.Now)
                throw new ArgumentException("Appointment date cannot be in the past");

            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason
            };

            var result = await _repository.Add(entity);

            return MapToDto(result);
        }

        // ==========================
        // Update
        // ==========================
        public async Task<AppointmentDto?> UpdateAppointment(int id, UpdateAppointmentDto dto)
        {
            if (id <= 0) return null;

            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason
            };

            var result = await _repository.Update(id, entity);
            if (result == null) return null;

            return MapToDto(result);
        }

        // ==========================
        // Delete
        // ==========================
        public async Task<bool> DeleteAppointment(int id)
        {
            if (id <= 0) return false;

            return await _repository.Delete(id);
        }
    }
}