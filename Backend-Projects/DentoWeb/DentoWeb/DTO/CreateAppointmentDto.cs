namespace DentoWeb.DTOs
{
    public class CreateAppointmentDto
    {
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
    }
}