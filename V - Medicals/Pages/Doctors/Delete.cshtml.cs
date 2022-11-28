using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Doctors
{
    public class DeleteModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DeleteModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Doctor Doctor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.DoctorId == id);

            if (doctor == null)
            {
                return NotFound();
            }
            else 
            {
                Doctor = doctor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor != null)
            {
                Doctor = doctor;
                Doctor.IsDeleted = true;
                _context.Doctors.Update(Doctor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
