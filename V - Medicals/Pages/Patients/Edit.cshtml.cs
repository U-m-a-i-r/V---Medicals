using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Patients
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public EditModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        //[BindProperty]
        public Patient Patient { get; set; } = default!;
        [BindProperty]
        public PatientViewModel InputModel { get; set; } = default!;
        [BindProperty]
        public int? PatientId { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient =  await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            Patient = patient;
            PatientId = id;
            InputModel = new PatientViewModel()
            {
                Title = Patient.Title,
                CNIC = Patient.CNIC,
                Address = new AddressModel()
                {
                    AddressLine = Patient.AddressLine!,
                    City = Patient.City,
                    District = Patient.District,
                    PostalCode = Patient.PostalCode,

                },
                DOB = Patient.DOB,
                Documents = Patient.Documents,
                Email = Patient.Email,
                FirstName = Patient.FirstName!,
                Gender = Patient.Gender,
                LastName = Patient.LastName!,
                MiddleName = Patient.MiddleName,
                PhoneNumber = Patient.PhoneNumber
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == PatientId && m.IsDeleted == false);
            if (patient == null)
            {
                return NotFound();
            }
            Patient = patient;
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity.Name;
            Patient.Title = InputModel.Title;
            Patient.PhoneNumber = InputModel.PhoneNumber;
            Patient.AddressLine = InputModel.Address!.AddressLine;
            Patient.City = InputModel.Address!.City;
            Patient.CNIC = InputModel.CNIC;
            Patient.ModefiedBy = userName;
            Patient.UpdatedOn = DateTime.UtcNow;
            Patient.District = InputModel.Address!.District;
            Patient.DOB = InputModel.DOB;
            Patient.Email = InputModel.Email;
            Patient.FirstName = InputModel.FirstName;
            Patient.Gender = InputModel.Gender;
            Patient.IsDeleted = false;
            Patient.LastName = InputModel.LastName;
            Patient.MiddleName = InputModel.MiddleName;
            Patient.PostalCode = InputModel.Address!.PostalCode;
            _context.Patients.Update(Patient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(Patient.PatientId))
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

        private bool PatientExists(int id)
        {
          return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}
