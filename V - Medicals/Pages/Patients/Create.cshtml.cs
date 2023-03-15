using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.Services;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Patients
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity.Name;
            //    var latestMRNumber = _context.Patients
            //.OrderByDescending(p => p.MRNumber)
            //.FirstOrDefault()?.MRNumber;
            var latestMRNumber = _context.Patients
                        .OrderByDescending(p => Convert.ToInt32(p.MRNumber.Substring(3)))
                        .FirstOrDefault()?.MRNumber;
            if (string.IsNullOrEmpty(latestMRNumber))
            {
                latestMRNumber = "VM-0";
            }
            var latestMRNumberWithoutPrefix = latestMRNumber.Substring(3);
            var newMRNumber = int.Parse(latestMRNumberWithoutPrefix) + 1;
            var newMRNumberString = "VM-" + newMRNumber.ToString();

            Patient patient = new Patient()
            {
                Title = InputModel.Title,
                MRNumber = newMRNumberString,
                PhoneNumber = InputModel.PhoneNumber,
                AddressLine = InputModel.Address!.AddressLine,
                City = InputModel.Address!.City,
                CNIC = InputModel.CNIC,
                CreatedBy = userName,
                CreatedOn = DateTime.UtcNow,
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
