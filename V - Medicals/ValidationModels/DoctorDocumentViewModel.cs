
using System.ComponentModel.DataAnnotations;

namespace V___Medicals.ValidationModels
{
    public class DoctorDocumentViewModel
    {
        [Required(ErrorMessage = "Document name is Required")]
        public string DocumentName { get; set; }
        [Required(ErrorMessage = "Please choose the file first!")]
        public IFormFile Document { get; set; }
    }
}
