using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Doctors.Documents
{
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DoctorDocument> DoctorDocument { get;set; } = default!;
        public Doctor Doctor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DoctorDocuments == null)
            {
                return NotFound();
            }
            Doctor = _context.Doctors.Where(d => d.DoctorId == id && d.IsDeleted==false).FirstOrDefault();
            if(Doctor == null)
            {
                return NotFound();
            }
            DoctorDocument = await _context.DoctorDocuments.Where(d=>d.DoctorId== id).Include(d => d.Doctor).ToListAsync();
            return Page();
            
        }
    }
}
