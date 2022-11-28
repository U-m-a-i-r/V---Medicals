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

namespace V___Medicals.Pages.Doctors.Documents
{
    public class EditModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public EditModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoctorDocument DoctorDocument { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DoctorDocuments == null)
            {
                return NotFound();
            }

            var doctordocument =  await _context.DoctorDocuments.FirstOrDefaultAsync(m => m.DoctorDocumentId == id);
            if (doctordocument == null)
            {
                return NotFound();
            }
            DoctorDocument = doctordocument;
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

            _context.Attach(DoctorDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorDocumentExists(DoctorDocument.DoctorDocumentId))
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

        private bool DoctorDocumentExists(int id)
        {
          return _context.DoctorDocuments.Any(e => e.DoctorDocumentId == id);
        }
    }
}
