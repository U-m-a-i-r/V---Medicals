using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Roles
{
    [Authorize]
    public class IndexModel : PageModel   
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        public IndexModel (UserManager<User> userManager, ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }
        public IList<IdentityRole<string>> Roles { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.UserRoles != null)
            {
               Roles = await _context.Roles.ToListAsync();
            }
        }
    }
}
