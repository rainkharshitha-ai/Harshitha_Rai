using DentoWeb.Data;
using DentoWeb.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PatientController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Patient created successfully",
            data = patient
        });
    }
}