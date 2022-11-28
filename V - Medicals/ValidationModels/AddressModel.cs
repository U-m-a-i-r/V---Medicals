using System.ComponentModel.DataAnnotations;

namespace V___Medicals.ValidationModels
{
    public class AddressModel
    {
        [Required(ErrorMessage = "Please enter the address line.")]
        [MinLength(5)]
        public string AddressLine { get; set; }
        public String? District { get; set; }
        public String? City { get; set; }
        public String? PostalCode { get; set; }
    }
}
