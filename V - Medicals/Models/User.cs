using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace V___Medicals.Models
{
    public class User : IdentityUser<string>
    {
        /*[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }*/


        [StringLength(50)]
        public string Name { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime? Updated { get; set; }
        public string? Token { get; set; }
        public string? ProfilePicture { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

        [Required]
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModefiedBy { get; set; }
        //public MaintainRecord maintainRecord { get; set; } = default!;
    }
}