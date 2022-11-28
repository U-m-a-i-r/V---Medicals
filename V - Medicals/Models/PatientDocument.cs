using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V___Medicals.Models
{
    public class PatientDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientDocumentId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [Required]
        public string DocumentName { get; set; }
        [Required]
        public string DocumentPath { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
