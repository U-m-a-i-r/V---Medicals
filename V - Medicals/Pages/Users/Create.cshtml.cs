using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(ApplicationDbContext context, RoleManager<IdentityRole<string>> roleManager, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public IList<IdentityRole<string>> Roles { get; set; } = default!;
        [BindProperty]
        public IList<Doctor> Doctors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserRoles != null)
            {
                Roles = await _context.Roles.ToListAsync();
                Doctors = await _context.Doctors.Where(d=>d.IsDeleted==false).ToListAsync();
            }
        }

        [BindProperty]
        public UserRegisterModel User { get; set; }
        [BindProperty]
        public String RoleName { get; set; }
        [BindProperty]
        public String SelectedDoctorId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userByName = await _userManager.FindByNameAsync(User.UserName);
            if (userByName != null)
            {
                ModelState.AddModelError(nameof(UserRegisterModel.UserName), "Username already registed.");
                return Page();
            }
            User user = new User();
            Doctor doctor = null;
            string uniqueFileName = null;
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
                    user.Created = DateTime.UtcNow;
                    user.Updated = DateTime.UtcNow;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    user.UserName = User.UserName.Trim();
                    user.ProfilePicture = doctor.ProfilePicture;
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
                user.Created = DateTime.UtcNow;
                user.Updated = DateTime.UtcNow;
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
                    _context.Doctors.Update(doctor);
                    _context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError(nameof(RoleName), "Please select a valid user's role.");
                    return Page();
                }
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
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
