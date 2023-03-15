using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V___Medicals.Constants;

namespace V___Medicals.Models
{
	public class Invoice
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; } = default!;
        [Required]
        public double TotalAmount { get; set; }
        public double? PaidAmount { get; set; }
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PaymentDate { get; set; }
        public bool isPaidToDoctor { get; set; } = false;
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModefiedBy { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}

