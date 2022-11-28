namespace V___Medicals.Models
{
    public class DoctorClinic
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }
}
