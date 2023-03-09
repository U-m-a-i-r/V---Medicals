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

namespace V___Medicals.Pages.Availability
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DetailsModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public V___Medicals.Models.Availability Availability { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Availabilities == null)
            {
                return NotFound();
            }

            var availability = await _context.Availabilities.Include(a => a.Clinic)
                .Include(a => a.Doctor).FirstOrDefaultAsync(m => m.AvailabilityId == id);
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
    }
}
