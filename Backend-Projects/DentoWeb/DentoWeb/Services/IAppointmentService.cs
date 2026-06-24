using DentoWeb.DTOs;

namespace DentoWeb.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointments();
        Task<AppointmentDto?> GetAppointmentById(int id);
        Task<AppointmentDto> CreateAppointment(CreateAppointmentDto dto);
        Task<AppointmentDto?> UpdateAppointment(int id, UpdateAppointmentDto dto);
        Task<bool> DeleteAppointment(int id);
    }
}