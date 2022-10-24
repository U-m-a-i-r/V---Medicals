using System.ComponentModel.DataAnnotations;

namespace V___Medicals.ValidationModels
{
    public class AddressModel
    {
        [Required(ErrorMessage = "Please enter the address line 1.")]
        [MinLength(5)]
        public string AddressLine1 { get; set; }
        [Required(ErrorMessage = "Please enter the address line 1.")]
        [MinLength(5)]
        public string? AddressLine2 { get; set; }
        [Required]
        public String District { get; set; }
        public String? City { get; set; }
        public String? Region { get; set; }
        public String? PostalCode { get; set; }
    }
}
