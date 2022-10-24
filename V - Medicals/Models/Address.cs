using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace V___Medicals.Models
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        public String? AddressLine1 { get; set; }
        public String? AddressLine2 { get; set; }
        public String? District { get; set; }
        public String? City { get; set; }
        public String? Region { get; set; }
        public String? PostalCode { get; set; }

        [ForeignKey("PatientId")]
        public int? PatientId { get; set; }

        // public Patient? Patient { get; set; }
        [ForeignKey("DoctorId")]
        public int? DoctorId { get; set; }

        //public Doctor? Doctor { get; set; }
    }
}
