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
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<V___Medicals.Models.Availability> Availability { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Availabilities != null)
            {
                Availability = await _context.Availabilities
                .Include(a => a.Clinic)
                .Include(a => a.Doctor).ToListAsync();
            }
        }
    }
}
