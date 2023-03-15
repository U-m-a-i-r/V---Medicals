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
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Doctors
{
    [Authorize]
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
        ViewData["SpecialityId"] = new SelectList(_context.Specialities.Where(s=>s.IsActive==true), "SpecialityId", "Name");
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
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity.Name;
            Doctor doctor = new Doctor()
            {
                Title = InputModel.Title,
                FirstName = InputModel.FirstName,
                MiddleName = InputModel.MiddleName,
                LastName = InputModel.LastName,
                SpecialityId = Speciality.SpecialityId,
                Qualification = InputModel.Qualification,
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
                VideoConsultancyCharges = InputModel.VideoConsultancyCharges,
                VideoConsultancyPercentage = InputModel.VideoConsultancyPercentage,
                PhysicalConsultancyCharges = InputModel.PhysicalConsultancyCharges,
                PhysicalConsultancyPercentage = InputModel.PhysicalConsultancyPercentage,
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = userName
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
                string uploadsFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                string filePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return "Files/"+uniqueFileName;
        }
    }
}
