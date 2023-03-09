using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Models;

namespace V___Medicals.Pages.Home
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalPatients { get; set; } = default!;
        public int TotalDoctors { get; set; } = default!;
        public int TotalAppointments { get; set; } = default!;
        public IList<Appointment> lastWeekAppointments { get; set; } = default!;

        public async Task OnGetAsync()
        {
            TotalPatients =  _context.Patients.Where(p => p.IsDeleted == false).Count();
            TotalDoctors = _context.Doctors.Where(d=>d.IsDeleted==false).Count();
            TotalAppointments = _context.Appointments.Count();
            lastWeekAppointments = await _context.Appointments.Where(apt => apt.CreatedOn! >= DateTime.Now.AddDays(-7)).ToListAsync();


        }
    }
}
