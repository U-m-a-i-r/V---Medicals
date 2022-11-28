using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace V___Medicals.ValidationModels
{
    public class EmailViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(64)]
        public string Email { get; set; }
    }
    public class SpecialityIdViewModel
    {
     
        [Display(Name = "SpecialityId")]
       
        public int SpecialityId { get; set; }
    }
    public class DoctorIdViewModel
    {

        [Display(Name = "DoctorId")]

        public int DoctorId { get; set; }
    }
    public class PatientIdViewModel
    {

        [Display(Name = "PatientId")]

        public int PatientId { get; set; }
    }
}
