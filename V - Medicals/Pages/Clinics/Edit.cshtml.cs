using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Clinics
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public EditModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

       // [BindProperty]
        public Clinic Clinic { get; set; } = default!;
        [BindProperty]
        public ClinicViewModel InputModel { get; set; } = default!;
        [BindProperty]
        public int? ClinicId { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Clinic == null)
            {
                return NotFound();
            }

            var clinic =  await _context.Clinic.FirstOrDefaultAsync(m => m.ClinicId == id);
            if (clinic == null)
            {
                return NotFound();
            }
            Clinic = clinic;
            ClinicId = id;
            InputModel = new ClinicViewModel()
            {
                Name = Clinic.Name,
                AddressLine = Clinic.AddressLine,
                City = Clinic.City,
                District = Clinic.District,
                MapLink = Clinic.MapLink,
                PostalCode = Clinic.PostalCode,
                Status = Clinic.Status,
                Summary = Clinic.Summary,
                Type = Clinic.Type

            };
            ViewData["Doctor"] = new SelectList(_context.Doctors.Where(d => d.IsDeleted == false && d.Status == DoctorStatusTypes.Active), "DoctorId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.

        [BindProperty]
        public int DoctorId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var clinic = await _context.Clinic.FirstOrDefaultAsync(m => m.ClinicId == ClinicId);
            if (clinic == null)
            {
                return NotFound();
            }
            Clinic = clinic;
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity.Name;
            Clinic.AddressLine = InputModel.AddressLine;
            Clinic.City = InputModel.City;
            Clinic.District = InputModel.District;
            Clinic.MapLink = InputModel.MapLink;
            Clinic.Name = InputModel.Name;
            Clinic.Status = InputModel.Status;
            Clinic.PostalCode = InputModel.PostalCode;
            Clinic.Summary = InputModel.Summary;
            Clinic.Type = InputModel.Type;
            Clinic.ModefiedBy = userName;
            Clinic.UpdatedOn = DateTime.UtcNow;

            _context.Clinic.Update(Clinic);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicExists(Clinic.ClinicId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClinicExists(int id)
        {
          return _context.Clinic.Any(e => e.ClinicId == id);
        }
    }
}
