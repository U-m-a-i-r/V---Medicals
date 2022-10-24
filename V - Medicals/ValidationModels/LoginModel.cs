using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace V___Medicals.ValidationModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is Required")]
        [MinLength(3, ErrorMessage = "Please enter a valid username")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters are required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are required")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }=false;
    }
}
