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

namespace V___Medicals.Pages.Doctors.Documents
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DeleteModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DoctorDocument DoctorDocument { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DoctorDocuments == null)
            {
                return NotFound();
            }

            var doctordocument = await _context.DoctorDocuments.FirstOrDefaultAsync(m => m.DoctorDocumentId == id);

            if (doctordocument == null)
            {
                return NotFound();
            }
            else 
            {
                DoctorDocument = doctordocument;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DoctorDocuments == null)
            {
                return NotFound();
            }
            var doctordocument = await _context.DoctorDocuments.FindAsync(id);

            if (doctordocument != null)
            {
                DoctorDocument = doctordocument;
                _context.DoctorDocuments.Remove(DoctorDocument);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
