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

namespace V___Medicals.Pages.Specialities
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
      public Speciality Speciality { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities.FirstOrDefaultAsync(m => m.SpecialityId == id);

            if (speciality == null)
            {
                return NotFound();
            }
            else 
            {
                Speciality = speciality;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }
            var speciality = await _context.Specialities.FindAsync(id);

            if (speciality != null)
            {
                Speciality = speciality;
                _context.Specialities.Remove(Speciality);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
