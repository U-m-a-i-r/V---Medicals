using System.ComponentModel.DataAnnotations.Schema;
using V___Medicals.Models;

namespace V___Medicals.ValidationModels
{
    public class PatientVitalsViewModel
    {
        public int AppointmentId { get; set; }
       

        public string? Weight { get; set; }
        public string? Height { get; set; }
        public string? BMI { get; set; }
        public string? Temprature { get; set; }
        public string? HeartRate { get; set; }
        public string? SystolicBP1 { get; set; }
        public string? DiastolicBP1 { get; set; }
    }
}
