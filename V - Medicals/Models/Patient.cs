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

        public Title? Title { get; set; }
        public String? FirstName { get; set; }
        public String? MiddleName { get; set; }
        public String? LastName { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1} {2}", Title, FirstName, LastName); }
        }

        public Gender? Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [DataType(DataType.EmailAddress)]
        public String? Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public String? PhoneNumber { get; set; }
        public string? CNIC { get; set; }
        public String? AddressLine { get; set; }
        public String? District { get; set; }
        public String? City { get; set; }
        public String? PostalCode { get; set; }
        public ICollection<PatientDocument>? Documents { get; set; }

        public String? UserId { get; set; }
        [ForeignKey("Id")]
        public User? User { get; set; }

        public bool? IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public String? LastModifiedBy { get; set; }
    }
}

