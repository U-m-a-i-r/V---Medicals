using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using V___Medicals.Models;

namespace V___Medicals.ValidationModels
{
    public class DoctorViewModel
    {
        [Required (ErrorMessage ="Please select the title")]
        public Title Title { get; set; }
        [Required (ErrorMessage ="Please enter the First Name")]
        public String FirstName { get; set; }

        public String? MiddleName { get; set; }
        [Required(ErrorMessage = "Please enter the Last Name")]
        public String LastName { get; set; }
        [Required(ErrorMessage = "Please select the gender")]
        public Gender Gender { get; set; }
        [Required]
        public int SpecialityId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }

        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }
        public String? AddressLine { get; set; }
        public String? District { get; set; }
        public String? City { get; set; }
        public String? PostalCode { get; set; }
        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePicture { get; set; }
        public ICollection<DoctorDocument>? Documents { get; set; }
        [Required(ErrorMessage = "Qualification field is required")]
        public String Qualification { get; set; }
        [MaxLength(5000)]
        public string? Discription { get; set; }
        [Required]
        public DoctorStatusTypes Status { get; set; }
    }
}
