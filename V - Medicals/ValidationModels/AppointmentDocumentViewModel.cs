
using System.ComponentModel.DataAnnotations;
using V___Medicals.Models;

namespace V___Medicals.ValidationModels
{
	public class AppointmentDocumentViewModel
	{
        public int AppointmentId { get; set; }
        //[Required(ErrorMessage = "Document name is Required")]
        //public string DocumentName { get; set; }
        public AppointmentDocumentType type { get; set; }
        [Required(ErrorMessage = "Please choose the file first!")]
        public List<IFormFile> Documents { get; set; }
        
	}
}