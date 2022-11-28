using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace V___Medicals.Models
{
    [Table("Speciality")]
    public class Speciality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecialityId { get; set; }
        [Required]
        public String Name { get; set; }
        
        //public FormFile icon { get; set; }
        [Required(ErrorMessage = "Please choose icon")]
        public String Icon{ get; set; }
        public bool IsActive { get; set; } = true;
    }
}   
