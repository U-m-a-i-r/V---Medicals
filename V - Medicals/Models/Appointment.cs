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
        [DisplayFormat(DataFormatString = "{'HH:mm:tt'}")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public ICollection<AppointmentDocument>? Documents { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime UpdatedOn { get; set; }
        public String? LastModifiedBy { get; set; }

    }
}
public enum AppointmentStatus
{
    OutStanding_Examination,
    Pending_Approval,
    Completed,
    Hold,
    Unpaid_Invoice,
    Paid_Invoice
}
