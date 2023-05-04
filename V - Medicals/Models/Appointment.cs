using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V___Medicals.Models
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
      
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        [DataType(DataType.Date)]
        public DateTime ClinicDate { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{'HH:mm:tt'}")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public ICollection<AppointmentDocument>? Documents { get; set; }
        public PatientVitals? PatientVitals { get; set; }
        //public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
        public ClinicTypes? AppointmentType { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? AdminNotes { get; set; }
        public string SpecialityName { get; set; } = default!;
        public string? DoctorNotes { get; set; }
        public string? PatientNotes { get; set; }
        //public DateTime UpdatedOn { get; set; }
        //public String? LastModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModefiedBy { get; set; }

    }
}
public enum AppointmentStatus
{
    OutStanding_Examination,
    Pending_Approval,
    Completed,
    Hold,
    Unpaid_Invoice,
    Paid_Invoice,
    DNA,
    Cancelled
}
