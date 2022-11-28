using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Clinics
{
    public class EditModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public EditModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Clinic Clinic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Clinic == null)
            {
                return NotFound();
            }

            var clinic =  await _context.Clinic.FirstOrDefaultAsync(m => m.ClinicId == id);
            if (clinic == null)
            {
                return NotFound();
            }
            Clinic = clinic;
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

            _context.Attach(Clinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(Clinic.ClinicId))
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

        private bool ClinicExists(int id)
        {
          return _context.Clinic.Any(e => e.ClinicId == id);
        }
    }
}
