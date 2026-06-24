using DentoWeb.Models;

namespace DentoWeb.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAll();
        Task<Appointment?> GetById(int id);
        Task<Appointment> Add(Appointment appointment);
        Task<Appointment?> Update(int id, Appointment appointment);
        Task<bool> Delete(int id);
    }
}