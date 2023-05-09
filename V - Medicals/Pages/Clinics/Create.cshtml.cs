using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.Services;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Clinics
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Doctor"] = new SelectList(_context.Doctors.Where(d=>d.IsDeleted==false && d.Status== DoctorStatusTypes.Active), "DoctorId", "FullName");
            return Page();
        }

        [BindProperty]
        public ClinicViewModel InputModel { get; set; }
        [BindProperty]
        public int DoctorId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
          Doctor doctor = _context.Doctors.Where(d=>d.DoctorId== DoctorId).Where(d=>d.IsDeleted==false && d.Status==DoctorStatusTypes.Active).FirstOrDefault()!;
            if (doctor == null)
            {
                ModelState.AddModelError(InputModel.AddressLine, "Please select a valid doctor!");
                return Page();
            }
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity!.Name;
            if (userName == null)
                throw new Exception("Logged in User is null");
            var loggedInUser = _context.Users.Where(user => user.UserName == userName).FirstOrDefault();
            if (loggedInUser == null)
                throw new Exception("Logged in Username is null");
            
            Clinic clinic = new Clinic {
                 AddressLine = InputModel.AddressLine,
                 City = InputModel.City,
                 District = InputModel.District,
                 MapLink = InputModel.MapLink,
                 Name = InputModel.Name,
                 Status = InputModel.Status,
                 PostalCode = InputModel.PostalCode,
                 Summary = InputModel.Summary,
                 Type = InputModel.Type,
                 CreatedBy = loggedInUser.Name,
                  Longitude= InputModel.Longitude??null,
                   Latitude= InputModel.Latitude??null,
                 CreatedOn = DateTime.UtcNow
            };
            _context.Clinic.Add(clinic);
            DoctorClinic doctorClinic = new DoctorClinic
            {
                Clinic = clinic,
                ClinicId = clinic.ClinicId,
                Doctor = doctor,
                DoctorId = doctor.DoctorId

            };
            _context.DoctorClinics.Add(doctorClinic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
