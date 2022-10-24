using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace V___Medicals.Models
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        public String? Title { get; set; }
        public String? FirstName { get; set; }
        public String? MiddleName { get; set; }
        public String? LastName { get; set; }

        public String? Gender { get; set; }

        public DateTime? DOB { get; set; }

        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }

        public String UserId { get; set; }
        [ForeignKey("Id")]
        public User User { get; set; }

        public Address? Address { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public String? LastModifiedBy { get; set; }
    }
}

