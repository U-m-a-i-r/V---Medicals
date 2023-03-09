using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace V___Medicals.Pages.Doctors
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        //[BindProperty]
        public Doctor Doctor { get; set; } = default!;
        [BindProperty]
        public DoctorViewModel InputModel { get; set; } = default!;
        [BindProperty]
        public int? DoctorId { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor =  await _context.Doctors.Include(d=>d.Speciality).FirstOrDefaultAsync(m => m.DoctorId == id && m.IsDeleted==false);
            if (doctor == null)
            {
                return NotFound();
            }
            Doctor = doctor;
            DoctorId = id;
            InputModel = new DoctorViewModel()
            {
                FirstName = Doctor.FirstName,
                MiddleName = Doctor.MiddleName,
                LastName = Doctor.LastName,
                Title = Doctor.Title,
                AddressLine = Doctor.AddressLine,
                City = Doctor.City,
                 Discription = Doctor.Discription,
                  District = Doctor.District,
                   DOB = Doctor.DOB,
                    Email = Doctor.Email,
                     Gender = Doctor.Gender,
                      PhoneNumber = Doctor.PhoneNumber,
                      Status = Doctor.Status,
                      SpecialityId = Doctor.SpecialityId,
                      PostalCode = Doctor.PostalCode,
                       Qualification = Doctor.Qualification,
                       ContractType = Doctor.ContractType,
                       ContractValue = Doctor.ContractValue
            };
           ViewData["SpecialityId"] = new SelectList(_context.Specialities, "SpecialityId", "Name");
           ViewData["Id"] = new SelectList(_context.Users, "Id", "Id");
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
            var doctor = await _context.Doctors.Include(d => d.Speciality).FirstOrDefaultAsync(m => m.DoctorId == DoctorId && m.IsDeleted == false);
            if (doctor == null)
            {
                return NotFound();
            }
            Doctor = doctor;
            Speciality? speciality1 = await _context.Specialities.Where(d => d.SpecialityId == InputModel.SpecialityId).FirstOrDefaultAsync();
            Doctor.Speciality = speciality1!;
            Doctor.Title = InputModel.Title;
            Doctor.FirstName = InputModel.FirstName;
            Doctor.MiddleName = InputModel.MiddleName;
            Doctor.LastName = InputModel.LastName;
            Doctor.SpecialityId = speciality1!.SpecialityId;
            Doctor.Qualification = InputModel.Qualification;
            Doctor.AddressLine = InputModel.AddressLine;
            Doctor.City = InputModel.City;
            Doctor.District = InputModel.District;
            Doctor.DOB = InputModel.DOB;
            Doctor.Email = InputModel.Email;
            Doctor.PhoneNumber = InputModel.PhoneNumber;
            Doctor.PostalCode = InputModel.PostalCode;
            Doctor.ContractType = InputModel.ContractType;
            Doctor.ContractValue = InputModel.ContractValue;

            Doctor.Gender = InputModel.Gender;
            Doctor.Discription = InputModel.Discription;
            Doctor.Status = InputModel.Status;
            //ProfilePicture = uniqueFileName,
            Doctor.IsDeleted = false;
            
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity.Name;
            Doctor.UpdatedOn = DateTime.UtcNow;
            Doctor.ModefiedBy = userName;
            //  _context.Attach(Doctor).State = EntityState.Modified;
            _context.Doctors.Update(Doctor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(Doctor.DoctorId))
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

        private bool DoctorExists(int id)
        {
          return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}
