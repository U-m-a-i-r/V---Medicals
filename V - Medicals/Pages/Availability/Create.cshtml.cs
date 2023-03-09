using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Availability
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetClinic(int id)
        {
            DoctorId=id;
            ViewData["ClinicId"] = new SelectList(_context.DoctorClinics.Where(dc=>dc.DoctorId== id).Include(dc=>dc.Clinic).Select(dc=>dc.Clinic), "ClinicId", "Name");
            var clinics = await _context.DoctorClinics.Where(dc => dc.DoctorId == id).Include(dc => dc.Clinic).Select(dc => dc.Clinic).ToListAsync();
            //return Page();
            //var data= clinics.ToJson();
            return  new JsonResult(clinics);
        }

        public IActionResult OnGet()
        {
        /*ViewData["ClinicId"] = new SelectList(_context.Clinic, "ClinicId", "AddressLine");*/
        ViewData["Doctor"] = new SelectList(_context.Doctors.Where(d => d.IsDeleted == false && d.Status == DoctorStatusTypes.Active), "DoctorId", "FullName");
            return Page();
        }
        [BindProperty]
        public int DoctorId { get; set; }
        [BindProperty]
        public int ClinicId { get; set; }

        [BindProperty]
        public AvailabilityViewModel InputModel { get; set; }
        public  IActionResult OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var StartTime = InputModel.StartTime;
            var EndTime = InputModel.EndTime;
            if(StartTime > EndTime)
            {
                return Page();
            }
            else
            {
                ClaimsPrincipal _user = HttpContext?.User!;
                var userName = _user.Identity.Name;
                var slotlenght = InputModel.SlotLenght;
                if (slotlenght > 0 && slotlenght<=60)
                {

                    V___Medicals.Models.Availability availability = new V___Medicals.Models.Availability()
                    {
                        DoctorId = InputModel.DoctorId,
                        ClinicDate = InputModel.ClinicDate,
                        StartTime = InputModel.StartTime,
                        EndTime = InputModel.EndTime,
                        SlotLenght = InputModel.SlotLenght,
                        ClinicId = InputModel.ClinicId,
                        Status = InputModel.Status,
                        CreatedOn= DateTime.UtcNow,
                        CreatedBy=userName,
                        BookedSlots = 0,
                        AvailableSlots = 0,
                    };
                    _context.Availabilities.Add(availability);
                    _context.SaveChanges();
                    var dtnext = StartTime;
                    int totalslots = 0;
                    while (dtnext < EndTime)
                    {
                        Slot slot = new Slot()
                        {
                            Availability = availability,
                            SlotLenght= availability.SlotLenght,
                            AvailabilityId = availability.AvailabilityId,
                            SlotTime =  dtnext,
                            Status = SlotStatus.Available,
                            CreatedBy = userName,
                            CreatedOn = DateTime.UtcNow

                        };
                        _context.Slots.Add(slot);
                        _context.SaveChanges();
                        totalslots++;
                        dtnext = dtnext.AddMinutes(slotlenght);
                    }
                    availability.AvailableSlots = totalslots;
                    _context.Availabilities.Update(availability);
                    _context.SaveChanges();

                }

            }

            return RedirectToPage("./Index");
        }
    }
}
