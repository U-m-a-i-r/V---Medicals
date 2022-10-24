using System.ComponentModel.DataAnnotations;

namespace V___Medicals.ValidationModels
{
    public class SpecialityViewModel
    {
        [Required (ErrorMessage ="Please enter the name of speciality.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please choose icon")]
        [Display(Name = "Speciality Icon")]
        public IFormFile Icon { get; set; }
    }
}
