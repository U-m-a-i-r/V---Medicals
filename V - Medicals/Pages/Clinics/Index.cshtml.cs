using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Clinics
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Clinic> Clinic { get;set; } = default!;
        public IList<DoctorClinic> DoctorClinic { get; set; } = default!;
        public int DoctorId { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Clinic != null && _context.DoctorClinics!=null)
            {
                Clinic = await _context.Clinic.ToListAsync();
                DoctorClinic = await _context.DoctorClinics.Include(dc=>dc.Doctor).Include(dc => dc.Clinic).ToListAsync();
                ViewData["DoctorId"] = new SelectList(_context.Doctors.Where(d => d.IsDeleted == false && d.Status == DoctorStatusTypes.Active), "DoctorId", "FullName");
            }
        }

        public async Task<PageResult> OnGetByDoctor(int id)
        {
            if (id == -1)
            {
                DoctorClinic = await _context.DoctorClinics.Include(dc => dc.Doctor).Include(dc => dc.Clinic).ToListAsync();
            }else
            {
                DoctorClinic = await _context.DoctorClinics.Where(dc=>dc.DoctorId ==id) .Include(dc => dc.Doctor).Include(dc => dc.Clinic).ToListAsync();
            }
            return Page();
            
        }
    }
}
