using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.Services;
using V___Medicals.ValidationModels;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace V___Medicals.Pages.Users
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(ApplicationDbContext context, IHttpContextAccessor httpContext, RoleManager<IdentityRole<string>> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public IList<IdentityRole<string>> Roles { get; set; } = default!;
        [BindProperty]
        public IList<Doctor> Doctors { get; set; } = default!;
        [BindProperty]
        public IList<Patient> Patients { get; set; } = default!;
        public  IActionResult OnGetAsync()
        {
            if (_context.Roles != null)
            {
                Roles =  _context.Roles.ToList();
                Doctors =  _context.Doctors.Where(d=>d.IsDeleted==false).Where(d=>d.Status==DoctorStatusTypes.Active).ToList();
                Patients =  _context.Patients.Where(p => p.IsDeleted == false).ToList();
            }
            return Page();
        }

        [BindProperty]
        public UserRegisterModel User { get; set; }
        [BindProperty]
        public String RoleName { get; set; }
        [BindProperty]
        public String? SelectedDoctorId { get; set; }
        [BindProperty]
        public String? SelectedPatientId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userByName = await _userManager.FindByNameAsync(User.UserName);
            if (userByName != null)
            {
                ModelState.AddModelError(nameof(UserRegisterModel.UserName), "Username already registered.");
                return Page();
            }
            User user = new User();
            ClaimsPrincipal _user = HttpContext?.User!;
            var userEmail = _user.GetUserEmail();
            var userNAME = _user.GetName();
            Doctor? doctor = null;
            Patient? patient = null;
            string? uniqueFileName = null;
            if (RoleName == Constants.Constants.ROLE_DOCTOR)
            {
                if(SelectedDoctorId == null)
                {
                    ModelState.AddModelError(nameof(SelectedDoctorId), "Please select a doctor.");
                    return Page();
                }
                doctor = _context.Doctors.Where(d => d.DoctorId == int.Parse(SelectedDoctorId)).FirstOrDefault();
                if (doctor != null)
                {
                    if (doctor.Id != null)
                    {
                        ModelState.AddModelError(nameof(SelectedDoctorId), "User already exist for this doctor!");
                        return Page();
                    }
                    user.Name = doctor.FullName;
                    user.PhoneNumber = doctor.PhoneNumber;
                    user.Email = doctor.Email;
                    user.CreatedOn = DateTime.UtcNow;
                    user.CreatedBy = userNAME;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    user.UserName = User.UserName.Trim();
                    user.ProfilePicture = doctor.ProfilePicture;
                    user.IsActive = true;
                }
            }
            else if (RoleName == Constants.Constants.ROLE_PATIENT)
            {
                if (SelectedPatientId == null)
                {
                    ModelState.AddModelError(nameof(SelectedPatientId), "Please select a patient.");
                    return Page();
                }
                patient = _context.Patients.Where(d => d.PatientId == int.Parse(SelectedPatientId)).FirstOrDefault()!;
                if (patient != null)
                {
                    if (patient.UserId != null)
                    {
                        ModelState.AddModelError(nameof(SelectedPatientId), "User already exist for this Patient!");
                        return Page();
                    }
                    if (User.ProfilePicture != null)
                    {
                        uniqueFileName = UploadedFile(User);
                    }
                    user.Name = patient.FullName;
                    user.PhoneNumber = patient.PhoneNumber;
                    user.Email = patient.Email;
                    user.CreatedOn = DateTime.UtcNow;
                    user.CreatedBy = userNAME;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    user.UserName = User.UserName.Trim();
                    user.ProfilePicture = uniqueFileName;
                    user.IsActive = true;
                }
            }
            else
            {
                if (User.Email != null)
                {
                    var userByEmail = await _userManager.FindByEmailAsync(User.Email);
                    if (userByEmail != null)
                    {
                        ModelState.AddModelError(nameof(UserRegisterModel.Email), "Email already Registered");
                        return Page();
                    }
                }
                
                if (User.ProfilePicture != null)
                {
                    uniqueFileName = UploadedFile(User);
                }
                if (User.PhoneNumber != null)
                {
                    user.PhoneNumber = User.PhoneNumber!.Trim();
                }
                if (User.Email != null)
                {
                    user.Email = User.Email!.Trim();
                }
                user.Name = User.Name!;
                user.CreatedOn = DateTime.UtcNow;
                user.CreatedBy = userNAME;
                //user.maintainRecord = maintainRecordResult.Entity;
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.UserName = User.UserName.Trim();
                
                user.ProfilePicture = uniqueFileName;
                user.IsActive = true;
            }
            if (RoleName != Constants.Constants.ROLE_ADMIN && RoleName != Constants.Constants.ROLE_DOCTOR && RoleName != Constants.Constants.ROLE_PATIENT)
            {
                ModelState.AddModelError(nameof(RoleName), "Please select a user's role.");
                return Page();
            }
            var result = await _userManager.CreateAsync(user, User.Password);
            if (result.Succeeded)
            {
                if (RoleName == Constants.Constants.ROLE_ADMIN)
                {
                    await _userManager.AddToRoleAsync(user, Constants.Constants.ROLE_ADMIN);
                }
                else if (RoleName == Constants.Constants.ROLE_DOCTOR)
                {
                    await _userManager.AddToRoleAsync(user, Constants.Constants.ROLE_DOCTOR);
                    doctor.Id = user.Id;
                    doctor.User = user;
                    doctor.ModefiedBy = userNAME;
                    doctor.UpdatedOn = DateTime.UtcNow;
                    _context.Doctors.Update(doctor);
                    _context.SaveChanges();
                }
                else if (RoleName == Constants.Constants.ROLE_PATIENT)
                {
                    await _userManager.AddToRoleAsync(user, Constants.Constants.ROLE_PATIENT);
                    patient!.UserId = user.Id;
                    patient.User = user;
                    patient.ModefiedBy = userNAME;
                    patient.UpdatedOn = DateTime.UtcNow;
                    _context.Patients.Update(patient);
                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError(nameof(RoleName), "Please select a valid user's role.");
                    return Page();
                }
                //user.maintainRecord = maintainRecord;
                //await _userManager.UpdateAsync(user);
                return RedirectToPage("./Index");

            }
            //_context.Users.Add(user);

            //await _context.SaveChangesAsync();
            ModelState.AddModelError(nameof(RoleName), result.Errors.ToString());
            return Page();
        }
        private string UploadedFile(UserRegisterModel model)
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
            return uniqueFileName;
        }
    }
}
