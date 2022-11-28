

using System.ComponentModel.DataAnnotations;

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
        //[DataType(DataType.Date)]
        /*public DateTime ClinicDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{'HH:mm:tt'}")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }*/
    }
}
