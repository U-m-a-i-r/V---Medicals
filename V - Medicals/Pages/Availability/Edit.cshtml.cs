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

namespace V___Medicals.Pages.Availability
{
    public class EditModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public EditModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public V___Medicals.Models.Availability Availability { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Availabilities == null)
            {
                return NotFound();
            }

            var availability =  await _context.Availabilities.FirstOrDefaultAsync(m => m.AvailabilityId == id);
            if (availability == null)
            {
                return NotFound();
            }
            Availability = availability;
           ViewData["ClinicId"] = new SelectList(_context.Clinic, "ClinicId", "AddressLine");
           ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName");
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

            _context.Attach(Availability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailabilityExists(Availability.AvailabilityId))
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

        private bool AvailabilityExists(int id)
        {
          return _context.Availabilities.Any(e => e.AvailabilityId == id);
        }
    }
}
