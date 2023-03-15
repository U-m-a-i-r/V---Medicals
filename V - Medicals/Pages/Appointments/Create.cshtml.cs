using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Appointments
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int SpecialityId { get; set; }
        public async Task<IActionResult> OnGetDoctors(int id)
        {
            SpecialityId = id;
            ViewData["DoctorId"] = new SelectList(_context.Doctors.Where(d=>d.SpecialityId==id &&  d.IsDeleted == false && d.Status == DoctorStatusTypes.Active), "DoctorId", "FullName");
            var doctors = await _context.Doctors.Where(d => d.SpecialityId == id && d.IsDeleted == false && d.Status == DoctorStatusTypes.Active).ToListAsync();
            //return Page();
            //var data= clinics.ToJson();
            return new JsonResult(doctors);
        }

        [BindProperty]
        public int DoctorId { get; set; }
        public async Task<IActionResult> OnGetClinic(int id)
        {
            DoctorId = id;
            ViewData["ClinicId"] = new SelectList(_context.Availabilities.Where(dc => dc.DoctorId == id).Include(dc => dc.Clinic), "AvailabilityId", "Name");
            var clinicsAvailabilities = await _context.Availabilities.Where(dc => dc.DoctorId == id).Include(dc => dc.Clinic).ToListAsync();
            //return Page();
            //var data= clinics.ToJson();
            return new JsonResult(clinicsAvailabilities);
        }
        [BindProperty]
        public int AvailabilityId { get; set; }
        public async Task<IActionResult> OnGetClinicSlots(int id)
        {
            AvailabilityId = id;
            ViewData["SlotId"] = new SelectList(_context.Slots.Where(slot=>slot.AvailabilityId==id && slot.Status ==SlotStatus.Available), "SlotId", "SlotTime");
            var slots = await _context.Slots.Where(slot => slot.AvailabilityId == id && slot.Status == SlotStatus.Available).ToListAsync();

            //foreach(var slot in slots)
            //{
            //    slot.SlotTime = slot.SlotTime.ToUniversalTime();
            //}
            //return Page();
            //var data= clinics.ToJson();
            return new JsonResult(slots);
        }


        public IActionResult OnGet()
        {
           // ViewData["DoctorId"] = new SelectList(_context.Doctors.Where(d=>d.IsDeleted==false && d.Status==DoctorStatusTypes.Active), "DoctorId", "FullName");
            ViewData["PatientId"] = new SelectList(_context.Patients.Where(p=>p.IsDeleted==false), "PatientId", "FullName");
            ViewData["SpecialityId"] = new SelectList(_context.Specialities.Where(s => s.IsActive == true), "SpecialityId", "Name");
            return Page();
        }

        [BindProperty]
        public AppointmentViewModelForWeb Appointment { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity!.Name;
            Patient patient = _context.Patients.Where(p => p.PatientId == Appointment.PateintId).FirstOrDefault()!;
            Doctor doctor = _context.Doctors.Where(p => p.DoctorId == Appointment.DoctorId).FirstOrDefault()!;
            Speciality speciality = _context.Specialities.Where(p => p.SpecialityId == Appointment.specialityId).FirstOrDefault()!;
            V___Medicals.Models.Availability availability = _context.Availabilities.Where(p => p.AvailabilityId == Appointment.availabilityId).FirstOrDefault()!;
            Slot slot = _context.Slots.Where(p => p.SlotId == Appointment.SlotId).FirstOrDefault()!;
            Appointment appointment = new Appointment() { DoctorId = doctor.DoctorId, Status = Appointment.Status, ClinicDate = availability.ClinicDate, PatientId = patient.PatientId, Time = slot.SlotTime, PatientNotes = Appointment.PatientNotes, AdminNotes = Appointment.AdminNotes, AppointmentType = Appointment.AppointmentType, CreatedBy = userName, CreatedOn = DateTime.UtcNow, Description = Appointment.Description, Doctor = doctor, DoctorNotes = Appointment.DoctorNotes, Patient = patient, SpecialityName = speciality .Name};
            var createdAppointment = await _context.Appointments.AddAsync(appointment);
            slot.Status = SlotStatus.Booked;
            slot.ModefiedBy = userName;
            slot.UpdatedOn = DateTime.UtcNow;
            _context.Slots.Update(slot);
            availability.BookedSlots = availability.BookedSlots + 1;
            availability.AvailableSlots = availability.AvailableSlots - 1;
            availability.UpdatedOn = DateTime.UtcNow;
            availability.ModefiedBy = userName;
            _context.Availabilities.Update(availability);
            //_context.SaveChanges();

            //_context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
