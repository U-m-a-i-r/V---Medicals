using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Constants;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        //private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AppointmentController(
        ApplicationDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            // _signInManager = signInManager;
        }
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert([FromBody] AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Patient patient =  _appDbContext.Patients.Where(p => p.PatientId == model.PateintId).FirstOrDefault();
                if (patient == null)
                {
                    return BadRequest(new Response {Status="Error",Message="Patient Id is wrong!" });
                }
                Doctor doctor = _appDbContext.Doctors.Where(p => p.DoctorId == model.DoctorId).FirstOrDefault();
                if (doctor == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Doctor Id is wrong!" });
                }
                Availability availability = _appDbContext.Availabilities.Where(p => p.AvailabilityId == model.availabilityId).FirstOrDefault();
                if (availability == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Wrong availability" });
                }
                Slot slot = _appDbContext.Slots.Where(p => p.SlotId == model.SlotId).FirstOrDefault();
                if (slot == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Slot Id is wrong!" });
                }
                if(slot.Status == SlotStatus.Booked)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Slot is already booked" });
                }

                Appointment appointment = new Appointment() {DoctorId =doctor.DoctorId,Status = AppointmentStatus.Pending_Approval,ClinicDate = availability.ClinicDate,CreatedDate = DateTime.UtcNow,PatientId = model.PateintId,Time = slot.SlotTime};
                var createdAppointment =  _appDbContext.Appointments.AddAsync(appointment);
                slot.Status = SlotStatus.Booked;
                _appDbContext.Slots.Update(slot);
                availability.BookedSlots = availability.BookedSlots+1;
                availability.AvailableSlots = availability.AvailableSlots - 1;
                _appDbContext.Availabilities.Update(availability);
                _appDbContext.SaveChanges();
                return StatusCode(201, new {  createdAppointment.Result.Entity });
            }
            else
            {
                return ValidationProblem();
            }


        }

        [HttpPost]
        [Route("getPatientAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> getPatientAppointments([FromBody] PatientIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                Patient patient = _appDbContext.Patients.Where(p => p.PatientId == model.PatientId).FirstOrDefault();
                if (patient == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Patient Id is wrong!" });
                }

                return await _appDbContext.Appointments.Where(a=>a.PatientId == model.PatientId).Include(a=>a.Doctor).ToListAsync();
            }
            else
            {
                return ValidationProblem();
            }


        }
        [HttpPost]
        [Route("getDoctorAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> getDoctorAppointments([FromBody] DoctorIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                Doctor doctor = _appDbContext.Doctors.Where(d=>d.DoctorId ==model.DoctorId && d.IsDeleted==false && d.Status ==DoctorStatusTypes.Active).FirstOrDefault();
                if (doctor == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Doctor Id is wrong!" });
                }

                return await _appDbContext.Appointments.Where(a => a.DoctorId == model.DoctorId).Include(a => a.Patient).ToListAsync();
            }
            else
            {
                return ValidationProblem();
            }


        }

    }
}
