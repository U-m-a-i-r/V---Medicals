

using System.ComponentModel.DataAnnotations;
using V___Medicals.Models;

namespace V___Medicals.ValidationModels
{
    public class AppointmentViewModel
    {
        [Required(ErrorMessage = "Patient ID is missing")]
        public int PateintId { get; set; }
        [Required(ErrorMessage = "Doctor ID is missing")]
        public int DoctorId { get; set; }
        public int availabilityId { get; set; }
        public int SlotId { get; set; }
        public ICollection<AppointmentDocumentViewModel>? Documents { get; set; }
        public PatientVitalsViewModel? PatientVitals { get; set; }
        //[DataType(DataType.Date)]
        /*public DateTime ClinicDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{'HH:mm:tt'}")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }*/
    }
    public class AppointmentViewModelForWeb
    {
        [Required(ErrorMessage = "Patient ID is missing")]
        public int PateintId { get; set; }
        [Required(ErrorMessage = "Doctor ID is missing")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Availability ID is missing")]
        public int availabilityId { get; set; }
        [Required(ErrorMessage = "Speciality ID is missing")]
        public int specialityId { get; set; }
        [Required(ErrorMessage = "Slot ID is missing")]
        public int SlotId { get; set; }
        public string? Description { get; set; }
        public string? AdminNotes { get; set; }
        public string? DoctorNotes { get; set; }
        public string? PatientNotes { get; set; }
        public ClinicTypes? AppointmentType { get; set; }
        public AppointmentStatus Status { get; set; }
        //[DataType(DataType.Date)]
        /*public DateTime ClinicDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{'HH:mm:tt'}")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }*/
    }
}
