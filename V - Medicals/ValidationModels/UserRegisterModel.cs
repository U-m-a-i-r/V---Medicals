using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace V___Medicals.ValidationModels
{
    public class UserRegisterModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters are required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are required")]
        public string Name { get; set; }


        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters are required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email Address is required.")]
        [Display(Name = "Email")]
        [MaxLength(64)]
        public string Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "Phone number is required")]
        [MinLength(10, ErrorMessage = "Phone number is not valid")]
        [MaxLength(14)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters are required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are required")]
        public string Password { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }

}