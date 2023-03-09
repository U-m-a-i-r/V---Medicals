﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public IndexModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;
        public IList<IdentityRole<string>?> Roles { get;set; } = default!;
        public IList<IDictionary<string, dynamic>> UserRoles { get; set; } = default!;
        IDictionary<string, dynamic> item { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users.Include(u=>u.Roles).ToListAsync();
                UserRoles = new List<IDictionary<string, dynamic>>();
                
                foreach (var user in User)
                {
                    foreach(var role in user.Roles)
                    {
                        var RoleId = role.RoleId;
                        var Role = _context.Roles.Where(r => r.Id == RoleId).FirstOrDefault();
                        item = new Dictionary<string, dynamic>();

                        item.Add(new KeyValuePair<string, dynamic>("UserId", user.Id));
                        item.Add(new KeyValuePair<string, dynamic>("UserRole", Role!.Name));
                        UserRoles.Add(item);
                       // Roles.Add(Role!);
                    }
                }            
            }
        }
    }
}