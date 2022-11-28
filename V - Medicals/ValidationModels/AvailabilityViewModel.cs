using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using V___Medicals.Models;
using V___Medicals.Constants;

namespace V___Medicals.ValidationModels
{
    public class AvailabilityViewModel
    {
        [Required]
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ClinicDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public int SlotLenght { get; set; }
        public StatusTypes Status { get; set; }
    }
}
