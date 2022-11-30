using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V___Medicals.Models
{
    public class AppointmentDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentDocumentId { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
        [Required]
        public string DocumentName { get; set; }
        [Required]
        public string DocumentPath { get; set; }
        public AppointmentDocumentType type { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
    }

    public enum AppointmentDocumentType
    {
        Prescriotion,
        History
    }
}
