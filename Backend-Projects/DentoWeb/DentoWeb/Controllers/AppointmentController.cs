using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;   

using DentoWeb.Data;
using DentoWeb.DTOs;
using DentoWeb.Models;

namespace DentoWeb.Controllers
{
    [Authorize]   
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDto dto)
        {
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == dto.PatientId);

            if (!patientExists)
                return BadRequest("Invalid PatientId");

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Appointment booked successfully",
                data = appointment
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Appointments.ToListAsync();
            return Ok(data);
        }
    }
}