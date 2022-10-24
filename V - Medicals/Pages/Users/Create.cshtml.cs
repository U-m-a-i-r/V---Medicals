using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

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

        public async Task OnGetAsync()
        {
            if (_context.UserRoles != null)
            {
                Roles = await _context.Roles.ToListAsync();
            }
        }

        [BindProperty]
        public UserRegisterModel User { get; set; }
        [BindProperty]
        public String RoleName { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var userByEmail = await _userManager.FindByEmailAsync(User.Email);
            if (userByEmail != null)
            {
                ModelState.AddModelError(nameof(UserRegisterModel.Email), "Email already Registered");
                return Page();
            }

            var userByName = await _userManager.FindByNameAsync(User.UserName);
            if (userByName != null)
            {
                ModelState.AddModelError(nameof(UserRegisterModel.UserName), "Username already registed.");
                return Page();
            }
            string uniqueFileName = null;
            if (User.ProfilePicture != null)
            {
                 uniqueFileName = UploadedFile(User);
            }
            if (RoleName != Constants.Constants.ROLE_ADMIN|| RoleName != Constants.Constants.ROLE_DOCTOR|| RoleName != Constants.Constants.ROLE_PATIENT)
            {
                ModelState.AddModelError(nameof(RoleName), "Please select a user's role.");
                return Page();
            }

            User user = new()
            {
                Name = User.Name,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Email = User.Email.Trim(),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = User.UserName.Trim(),
                PhoneNumber = User.PhoneNumber.Trim(),
                ProfilePicture = uniqueFileName,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, User.Password);
            if (result.Succeeded)
            {
                if (RoleName == Constants.Constants.ROLE_ADMIN)
                {
                    await _userManager.AddToRoleAsync(user, Constants.Constants.ROLE_ADMIN);
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
            ModelState.AddModelError(nameof(RoleName), "Something went wrong!");
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
