using System;
using System.ComponentModel.DataAnnotations;

namespace V___Medicals.ValidationModels
{
	public class PatientRegistrationModelFromMobile
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
        public string? PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }


        [Display(Name = "Date of Birth")]
        public DateTime? DOB { get; set; }
        public string? CNIC { get; set; }

        [Display(Name = "Address")]
        public AddressModel? Address { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters are required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are required")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters are required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are required")]
        public string Password { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}

