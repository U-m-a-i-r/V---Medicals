using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Doctors.Documents
{
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["DoctorId"] = new SelectList(_context.Doctors.Where(d=>d.IsDeleted==false), "DoctorId", "FullName");
            return Page();
        }

        [BindProperty]
        public DoctorDocumentViewModel InputModel { get; set; }
        [BindProperty]
        public int DoctorId { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var doctor = _context.Doctors.Where(d => d.DoctorId == DoctorId && d.IsDeleted==false).FirstOrDefault();
            if(doctor == null)
            {
                return NotFound();
            }
            var uniqueFileName = UploadedFile(InputModel);
            DoctorDocument doctorDocument = new DoctorDocument
            {
                DoctorId = doctor.DoctorId,
                Doctor = doctor,
                DocumentName = InputModel.DocumentName,
                DocumentPath = uniqueFileName,
                IsDeleted = false
            
            };

            _context.DoctorDocuments.Add(doctorDocument);
            doctor.Documents.Add(doctorDocument);
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
        private string UploadedFile(DoctorDocumentViewModel model)
        {
            string uniqueFileName = null;

            if (model.Document != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Document.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Document.CopyTo(fileStream);
                }
            }
            return "Files/" + uniqueFileName;
        }
    }
}
