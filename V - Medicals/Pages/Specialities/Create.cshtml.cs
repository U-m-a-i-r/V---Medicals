using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Specialities
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public CreateModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SpecialityViewModel model { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            string uniqueFileName = UploadedFile(model);

            Speciality speciality = new Speciality
            {
                Name = model.Name,
                Icon = uniqueFileName,
                IsActive = true
            };

            _context.Specialities.Add(speciality);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private string UploadedFile(SpecialityViewModel model)
        {
            string uniqueFileName = null;

            if (model.Icon != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Icon.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Icon.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
