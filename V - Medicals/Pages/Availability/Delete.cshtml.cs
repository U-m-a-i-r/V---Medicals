using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Availability
{
    public class DeleteModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DeleteModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public V___Medicals.Models.Availability Availability { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Availabilities == null)
            {
                return NotFound();
            }

            var availability = await _context.Availabilities.FirstOrDefaultAsync(m => m.AvailabilityId == id);

            if (availability == null)
            {
                return NotFound();
            }
            else 
            {
                Availability = availability;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Availabilities == null)
            {
                return NotFound();
            }
            var availability = await _context.Availabilities.FindAsync(id);

            if (availability != null)
            {
                Availability = availability;
                _context.Availabilities.Remove(Availability);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
