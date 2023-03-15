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

namespace V___Medicals.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; } = default!;
        public int AppointmentId { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Invoices != null)
            {
                Invoice = await _context.Invoices
                .Include(i => i.Appointment).Include(apt=>apt.Appointment.Patient).Include(apt => apt.Appointment.Doctor).ToListAsync();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments.Include(Appointments=>Appointments.Patient), "AppointmentId", "Patient.FullName");
        }
    }
}
