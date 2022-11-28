using System.ComponentModel.DataAnnotations;
using V___Medicals.Constants;

namespace V___Medicals.ValidationModels
{
    public class ClinicViewModel
    {
        [Required (ErrorMessage ="Please add the name of clinic")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Please add the Address of clinic")]
        public String AddressLine { get; set; }
        public String? District { get; set; }
        public String? City { get; set; }
        public String PostalCode { get; set; }
        public String? MapLink { get; set; }
        public String? Summary { get; set; }
        public ClinicTypes Type { get; set; }
        public StatusTypes Status { get; set; }
    }
}
