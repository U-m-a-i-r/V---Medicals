﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Clinics
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DeleteModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Clinic Clinic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Clinic == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinic.FirstOrDefaultAsync(m => m.ClinicId == id);

            if (clinic == null)
            {
                return NotFound();
            }
            else 
            {
                Clinic = clinic;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Clinic == null)
            {
                return NotFound();
            }
            var clinic = await _context.Clinic.FindAsync(id);

            if (clinic != null)
            {
                Clinic = clinic;
                _context.Clinic.Remove(Clinic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
