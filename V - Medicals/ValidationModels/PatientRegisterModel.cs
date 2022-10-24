using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace V___Medicals.ValidationModels
{
    public class PatientRegisterModel
    {
        //[Required(ErrorMessage = "Title Address is required.")]
        [Display(Name = "Title")]
        [MaxLength(12, ErrorMessage = ("Maximun 12 Characters"))]
        [MinLength(03, ErrorMessage = ("Minimum 3 Characters"))]
        public string? Title { get; set; }

        [Required(ErrorMessage = "FirstName Address is required.")]
        [Display(Name = "FirstName")]
        [MinLength(03, ErrorMessage = ("Minimum 3 Characters"))]
        public string FirstName { get; set; }

        [Display(Name = "MiddleName")]
        //[MinLength(03, ErrorMessage = ("Minimum 3 Characters"))]
        public string? MiddleName { get; set; } = String.Empty;

        [Required(ErrorMessage = "LastName Address is required.")]
        [Display(Name = "LastName")]
        [MinLength(03, ErrorMessage = ("Minimum 3 Characters"))]
        public string LastName { get; set; }

        [EmailAddress]
        // [Required(ErrorMessage = "Email Address is required.")]
        [Display(Name = "Email")]
        [MaxLength(64)]
        public string Email { get; set; }

        [Phone]
        // [Required(ErrorMessage = "Phone number is required")]
        [MinLength(10, ErrorMessage = "Minimum 10 characters are required")]
        [MaxLength(14, ErrorMessage = "Maximum 14 characters are required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get;
            set;
        }

        [Display(Name = "Gender")]
        //  [Required(ErrorMessage = "Gender is required")]
        [MinLength(4, ErrorMessage = "Minimum 4 characters are required")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters are required")]
        public string Gender { get; set; }


        [Display(Name = "Date of Birth")]
        //[Required(ErrorMessage = "DOB is required")]
        public DateTime DOB { get; set; }

        [Display(Name = "Address")]
        //  [Required(ErrorMessage = "Gender is required")]
        public AddressModel Address { get; set; }
    }
}

