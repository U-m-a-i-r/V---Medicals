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

namespace V___Medicals.Pages.Patients
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
            return Page();
        }

        [BindProperty]
        public PatientViewModel InputModel { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            Patient patient = new Patient()
            {
                Title = InputModel.Title,
                PhoneNumber = InputModel.PhoneNumber,
                AddressLine = InputModel.Address!.AddressLine,
                City = InputModel.Address!.City,
                CNIC = InputModel.CNIC,
                CreatedOn = DateTime.Now,
                District = InputModel.Address!.District,
                DOB = InputModel.DOB,
                Email = InputModel.Email,
                FirstName = InputModel.FirstName ,
                Gender = InputModel.Gender,
                IsDeleted = false,
                LastName = InputModel.LastName ,
                MiddleName = InputModel.MiddleName ,
                PostalCode = InputModel.Address!.PostalCode,
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
