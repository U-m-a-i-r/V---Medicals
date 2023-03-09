using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V___Medicals.Constants;
using V___Medicals.ValidationModels;

namespace V___Medicals.Models
{
    public class Clinic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClinicId { get; set; }
        public IList<DoctorClinic> DoctorClinics { get; set; }
        public IList<Availability> Availabilities { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String AddressLine { get; set; }
        public String? District { get; set; }
        public String? City { get; set; }
        public String PostalCode { get; set; }
        public String? MapLink { get; set; }
        public String? Summary { get; set; }
        public ClinicTypes Type { get; set; }
        public string Address
        {
            get { return string.Format("{0} {1} {2} {3}", AddressLine, District, City, PostalCode); }
        }
        public StatusTypes Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModefiedBy { get; set; }
    }
}
public enum ClinicTypes
{
    Online,
    Face_To_Face
}
