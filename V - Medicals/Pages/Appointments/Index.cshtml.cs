using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Appointments
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get;set; } = default!;

        public async Task<IActionResult> OnGetExportCsvAsync()
        {
            var appointments = await _context.Appointments.Include(apt=>apt.Patient).Include(apt => apt.Doctor).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Patient,Doctor,Date,Time,Speciality,Status");
            foreach (var appointment in appointments)
            {
                sb.AppendLine($"{appointment.Patient.FullName},{appointment.Doctor.FullName},{appointment.ClinicDate},{appointment.Time},{appointment.SpecialityName},{appointment.Status.ToString()}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "appointments.csv");
        }

        public async Task OnGetAsync()
        {
            if (_context.Appointments != null)
            {
                Appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient).ToListAsync();
                
            }
        }
    }
}
