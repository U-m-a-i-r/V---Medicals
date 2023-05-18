using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Appointments
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public EditModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;
      
        public Doctor doctor { get; set; } = default!;
      
        public Patient patient { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment =  await _context.Appointments.FirstOrDefaultAsync(m => m.AppointmentId == id);
            
            if (appointment == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == appointment.PatientId);
            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == appointment.DoctorId);
            Appointment = appointment;
            Appointment.Patient = patient!;
            Appointment.Doctor = doctor!;
           ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FullName");
           ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(Appointment.AppointmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AppointmentExists(int id)
        {
          return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}
