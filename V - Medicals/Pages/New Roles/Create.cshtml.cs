using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.NewRoles
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole<string>> _roleManager;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context, RoleManager<IdentityRole<string>> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public IdentityRole<string> Role { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (Role.Name==null)
            {
                ModelState.AddModelError(nameof(Role.Name), "Role name is empty!");
                return Page();
            }
            if (await _roleManager.RoleExistsAsync(Role.Name))
            {
                ModelState.AddModelError(Role.Name, "Role already registered");
                return Page();
               

            }

                IdentityResult result3 = await _roleManager.CreateAsync(new IdentityRole(Role.Name));
            //_context.Roles.Add(Role);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
