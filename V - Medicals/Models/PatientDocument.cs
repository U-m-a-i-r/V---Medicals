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
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModefiedBy { get; set; }
    }
}
