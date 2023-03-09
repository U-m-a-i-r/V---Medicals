using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Patients
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DetailsModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Patient Patient { get; set; }
        public IList<Appointment> Appointments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == id);
           
            if (patient == null)
            {
                return NotFound();
            }
            else 
            {
                Appointments = await _context.Appointments.Where(apt => apt.PatientId == patient.PatientId).Include(apt=>apt.Doctor).ToListAsync();
                Patient = patient;
            }
            return Page();
        }
    }
}
