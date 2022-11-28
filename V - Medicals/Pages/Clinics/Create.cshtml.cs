using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Clinics
{
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
          Doctor doctor = _context.Doctors.Where(d=>d.DoctorId== DoctorId).FirstOrDefault();
            if (doctor == null)
            {
                ModelState.AddModelError(InputModel.AddressLine, "Please select a valid doctor!");
                return Page();
            }
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
