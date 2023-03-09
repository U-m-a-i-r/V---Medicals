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

namespace V___Medicals.Pages.Availability
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<V___Medicals.Models.Availability> Availability { get;set; } = default!;
        public int DoctorId { get; set; } = default!;

        public async Task<IActionResult> OnGetAvailableSLots(int id)
        {
           
            var slots = await _context.Slots.Where(slot=>slot.AvailabilityId==id && slot.Status==SlotStatus.Available).Select(slot=>slot.SlotTime).ToListAsync();
            //return Page();
            //var data= clinics.ToJson();
            return new JsonResult(slots);
        }

        public async Task OnGetAsync()
        {
            if (_context.Availabilities != null)
            {
                Availability = await _context.Availabilities
                .Include(a => a.Clinic)
                .Include(a => a.Doctor).ToListAsync();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors.Where(d => d.IsDeleted == false && d.Status == DoctorStatusTypes.Active), "DoctorId", "FullName");
        }
    }
}
