using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V___Medicals.Models
{
    public class PatientVitals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientVitalsId { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
        
        public string? Weight { get; set; }
        public string? Height { get; set; }
        public string? BMI { get; set; }
        public string? Temprature { get; set; }
        public string? HeartRate { get; set; }
        public string? SystolicBP1 { get; set; }
        public string? DiastolicBP1 { get; set; }
        public DateTime AddedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
