using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace V___Medicals.Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

        public String Title { get; set; }
        public String FirstName { get; set; }
        public String? MiddleName { get; set; }
        public String LastName { get; set; }
        public String? Gender { get; set; }

        public int SpecialityId { get; set; }
        [ForeignKey("SpecialityId")]
        public Speciality Speciality { get; set; }


        public DateTime? DOB { get; set; }

        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }
        [Required]
        public String Status { get; set; }
        public bool IsDeleted { get; set; } = false;

        public String? Id { get; set; }
        [ForeignKey("Id")]
        public User? User { get; set; }
    }
}
