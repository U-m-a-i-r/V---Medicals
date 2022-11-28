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

namespace V___Medicals.Pages.Doctors
{
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["SpecialityId"] = new SelectList(_context.Specialities, "SpecialityId", "Name");
        ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public DoctorViewModel InputModel { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var Speciality = _context.Specialities.Where(s => s.SpecialityId == InputModel.SpecialityId).FirstOrDefault();
            string uniqueFileName = null;
            if (InputModel.ProfilePicture != null)
            {
                 uniqueFileName = UploadedFile(InputModel);
            }
            Doctor doctor = new Doctor()
            {
                Title = InputModel.Title,
                FirstName = InputModel.FirstName,
                MiddleName = InputModel.MiddleName,
                LastName = InputModel.LastName,
                SpecialityId = Speciality.SpecialityId,
                Qualification= InputModel.Qualification,
                Speciality = Speciality,
                AddressLine = InputModel.AddressLine,
                City = InputModel.City,
                District = InputModel.District,
                DOB = InputModel.DOB,
                Email = InputModel.Email,
                PhoneNumber = InputModel.PhoneNumber,
                PostalCode = InputModel.PostalCode,
                Gender = InputModel.Gender,
                Discription = InputModel.Discription,
                Status = InputModel.Status,
                ProfilePicture = uniqueFileName,
                IsDeleted = false,
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private string UploadedFile(DoctorViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return "Files/"+uniqueFileName;
        }
    }
}
