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
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Doctors != null)
            {
                Doctor = await _context.Doctors.Where(d=>d.IsDeleted==false).Include(d=>d.Speciality).Include(d => d.User).Include(d=>d.DoctorClinics).ThenInclude(c=>c.Clinic).ToListAsync();
                
            }
        }
    }
}
