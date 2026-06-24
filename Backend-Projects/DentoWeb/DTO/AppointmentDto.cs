namespace DentoWeb.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Reason { get; set; }
    }
}