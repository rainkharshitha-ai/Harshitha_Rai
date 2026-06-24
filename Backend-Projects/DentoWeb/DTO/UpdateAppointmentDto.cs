namespace DentoWeb.DTOs
{
    public class UpdateAppointmentDto
    {
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
    }
}