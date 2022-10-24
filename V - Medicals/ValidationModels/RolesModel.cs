using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace V___Medicals.ValidationModels
{
    public class RolesModel
    {

        [Required(ErrorMessage = "Role name is required.")]
        [Display(Name = "Role")]
        [MaxLength(20)]
        public string UserRole { get; set; }

        [Required]
        [Display(Name = "Status")]

        public bool IsActive { get; set; }
    }
}