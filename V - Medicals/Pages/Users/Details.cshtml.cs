using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Users
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DetailsModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;
        public IList<IDictionary<string, dynamic>> UserRoles { get; set; } = default!;
        IDictionary<string, dynamic> item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(u=>u.Roles).FirstOrDefaultAsync(m => m.Id == id);
            
            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                UserRoles = new List<IDictionary<string, dynamic>>();
                User = user;
                foreach (var role in User.Roles)
                {
                    var RoleId = role.RoleId;
                    var Role = _context.Roles.Where(r => r.Id == RoleId).FirstOrDefault();
                    item = new Dictionary<string, dynamic>();

                    item.Add(new KeyValuePair<string, dynamic>("UserId", User.Id));
                    item.Add(new KeyValuePair<string, dynamic>("UserRole", Role!.Name));
                    UserRoles.Add(item);
                    // Roles.Add(Role!);
                    Console.Out.NewLine=item.ToString();
                }
            }
            return Page();
        }
    }
}
