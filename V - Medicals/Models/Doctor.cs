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
        [Required]
        public Title Title { get; set; }
        [Required]
        public String FirstName { get; set; }
     
        public String? MiddleName { get; set; }
        [Required]
        public String LastName { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1} {2} {3}", Title, FirstName, MiddleName, LastName); }
        }
        [Required]
        public Gender Gender { get; set; }

        public int SpecialityId { get; set; }
        [ForeignKey("SpecialityId")]
        public Speciality Speciality { get; set; }


        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }

        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }
        public String? AddressLine { get; set; }
        public String? District { get; set; }
        public String? City { get; set; }
        public String? PostalCode { get; set; }
        public string Address
        {
            get { return string.Format("{0} {1} {2} {3}", AddressLine, District, City, PostalCode); }
        }
        public String? ProfilePicture { get; set; }
        public IList<DoctorDocument>? Documents { get; set; }
        public IList<DoctorClinic>? DoctorClinics { get; set; }

        [Required(ErrorMessage ="Qualification field is required")]
        public String Qualification { get; set; }
        [MaxLength(5000)]
        public string? Discription { get; set; }
        [Required]
        public DoctorStatusTypes Status { get; set; }
        [Required]
        public ContractType ContractType { get; set; }
        [Required]
        public String ContractValue { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;

        public String? Id { get; set; }
        [ForeignKey("Id")]
        public User? User { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModefiedBy { get; set; }

    }
}
public enum DoctorStatusTypes
{
    Active,
    Inactive,
    Awaiting_Documents,
    Awaiting_Contract
}
public enum Gender
{
    Male,
    Female
}
public enum ContractType
{
    Per_Appointment,
    Percentage
}
