using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using V___Medicals.Models;

namespace V___Medicals.ValidationModels
{
    public class PatientViewModel
    {
        [Display(Name = "Title")]
        public Title? Title { get; set; }

        [Required(ErrorMessage = "First Name  is required.")]
        [Display(Name = "First Name")]
        [MinLength(01, ErrorMessage = ("Minimum 1 Character"))]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        [MinLength(01, ErrorMessage = ("Minimum 1 Character"))]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set;}

        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }
        public string? CNIC { get; set; }
        public ICollection<PatientDocument>? Documents { get; set; }

        [Display(Name = "Address")]
        public AddressModel? Address { get; set; }
    }
}

