using System.Linq;
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
        
        public object avgAppointments { get; set; }
        public object appointmentsBySpeciality { get; set; }
        public object result { get; set; }
        public async Task<PageResult> OnGetAsync()
        {
            TotalPatients =  _context.Patients.Where(p => p.IsDeleted == false).Count();
            TotalDoctors = _context.Doctors.Where(d=>d.IsDeleted==false).Count();
            TotalAppointments = _context.Appointments.Count();
            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddDays(-6);
            var now = DateTime.UtcNow;
            var dates = Enumerable.Range(0, 7).Select(i => now.AddDays(-i)).Reverse().ToList();

            var appointmentsByDate = _context.Appointments
                .Where(a => a.CreatedOn.HasValue && a.CreatedOn.Value >= now.AddDays(-6))
                .GroupBy(a => a.CreatedOn!.Value.Date)
                .Select(g => new { date = g.Key, count = g.Count() })
                .ToList();

             result = dates.Select(d => new {
                date = d.ToString("yyyy-MM-dd"),
                count = appointmentsByDate.FirstOrDefault(a => a.date == d.Date)?.count ?? 0
            }).ToList();
             avgAppointments = appointmentsByDate.Average(a => a.count);

             appointmentsBySpeciality = _context.Appointments
    .Where(a => a.CreatedOn.HasValue && a.CreatedOn.Value >= now.AddDays(-6))
    .GroupBy(a => a.SpecialityName)
    .Select(g => new { speciality = g.Key, count = g.Count() })
    .ToList();

            return Page();

        }
    }
}
