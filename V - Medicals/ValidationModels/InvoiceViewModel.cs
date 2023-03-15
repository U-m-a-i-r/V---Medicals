using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using V___Medicals.Models;

namespace V___Medicals.ValidationModels
{
	public class InvoiceViewModel
	{
        [Required(ErrorMessage = "Appointment ID is missing!")]
        public int AppointmentId { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        public double? PaidAmount { get; set; }
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PaymentDate { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}


