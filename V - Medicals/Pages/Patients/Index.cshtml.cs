using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Patients
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Patient> Patient { get;set; } = default!;

        public async Task<IActionResult> OnGetExportCsvAsync()
        {
            var patients = await _context.Patients.Where(p=>p.IsDeleted==false).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("MR No,Full Name,DOB,Email,Phone No,CNIC");
            foreach (var patient in patients)
            {
                sb.AppendLine($"{patient.MRNumber},{patient.FullName},{patient.DOB},{patient.Email},{patient.PhoneNumber},{patient.CNIC}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "patients.csv");
        }

        public async Task OnGetAsync()
        {
            if (_context.Patients != null)
            {
                Patient = await _context.Patients.ToListAsync();
            }
        }
    }
}
