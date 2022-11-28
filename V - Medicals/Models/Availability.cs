using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V___Medicals.Constants;

namespace V___Medicals.Models
{
    public class Availability
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvailabilityId { get; set; }
        [Required]
        public int ClinicId { get; set; }
        [ForeignKey("ClinicId")]
        public Clinic Clinic { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        [DataType(DataType.Date)]
        public DateTime ClinicDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public int SlotLenght { get; set; }
        public int AvailableSlots { get; set; }
        public int BookedSlots { get; set; }
        [DataType(DataType.Time)]
        public IList<Slot> Slots { get; set; }
        public StatusTypes Status { get; set; }  
    }
}

