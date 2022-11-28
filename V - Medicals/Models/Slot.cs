using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace V___Medicals.Models
{
    public class Slot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlotId { get; set; }
        [Required]
        public int AvailabilityId { get; set; }
        [ForeignKey("AvailabilityId")]
        public Availability Availability { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{'HH:mm:tt'}")]
        [DataType(DataType.Time)]
        public DateTime SlotTime { get; set; }
        [Required]
        public int SlotLenght { get; set; }
        [Required]
        public SlotStatus Status { get; set; }
    }
}
public enum SlotStatus
{
    Booked,
    Available
}
