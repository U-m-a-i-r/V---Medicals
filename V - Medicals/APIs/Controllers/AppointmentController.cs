using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Constants;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.Services;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public AppointmentController(
        ApplicationDbContext appDbContext, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            // _signInManager = signInManager;
        }
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert([FromBody] AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Patient patient =  _appDbContext.Patients.Where(p => p.PatientId == model.PateintId).FirstOrDefault()!;
                if (patient == null)
                {
                    return BadRequest(new Response {Status="Error",Message="Patient Id is wrong!" });
                }
                Doctor doctor = _appDbContext.Doctors.Where(p => p.DoctorId == model.DoctorId).FirstOrDefault()!;
                if (doctor == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Doctor Id is wrong!" });
                }
                Availability availability = _appDbContext.Availabilities.Where(p => p.AvailabilityId == model.availabilityId).FirstOrDefault()!;
                if (availability == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Wrong availability" });
                }
                Speciality speciality = _appDbContext.Specialities.Where(sp => sp.SpecialityId == doctor.SpecialityId).FirstOrDefault()!;
                if (availability == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Wrong Speciality" });
                }
                Slot slot = _appDbContext.Slots.Where(p => p.SlotId == model.SlotId).FirstOrDefault()!;
                if (slot == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Slot Id is wrong!" });
                }
                if(slot.Status == SlotStatus.Booked)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Slot is already booked" });
                }
                var appUserId = User.GetLoggedInUserId<string>();
                if (appUserId == null)
                    throw new Exception("Logged in User is null");
                Appointment appointment = new Appointment() {DoctorId =doctor.DoctorId,Status = AppointmentStatus.OutStanding_Examination,ClinicDate = availability.ClinicDate,PatientId = model.PateintId,Time = slot.SlotTime, SpecialityName = speciality .Name};
                var createdAppointment = await  _appDbContext.Appointments.AddAsync(appointment);
                slot.Status = SlotStatus.Booked;
                _appDbContext.Slots.Update(slot);
                availability.BookedSlots = availability.BookedSlots+1;
                availability.AvailableSlots = availability.AvailableSlots - 1;
                _appDbContext.Availabilities.Update(availability);
                _appDbContext.SaveChanges();
                return StatusCode(201, new {  createdAppointment.Entity });
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

                return await _appDbContext.Appointments.Where(a=>a.PatientId == model.PatientId).Include(a=>a.Doctor).Include(a=>a.Documents).ToListAsync();
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

                return await _appDbContext.Appointments.Where(a => a.DoctorId == model.DoctorId && a.Status==AppointmentStatus.OutStanding_Examination).Include(a => a.Patient).ToListAsync();
            }
            else
            {
                return ValidationProblem();
            }


        }

        [HttpPost]
        [Route("InsertAppointmentVitals")]
        public async Task<ActionResult<PatientVitals>> InsertAppointmentVitals([FromBody] PatientVitalsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Appointment = _appDbContext.Appointments.Where(a => a.AppointmentId == model.AppointmentId).FirstOrDefault();
                if (Appointment == null) return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "Wrong appointment Id!"
                });
                ClaimsPrincipal _user = HttpContext?.User!;
                var userName = _user.Identity.Name;
                PatientVitals PatientVitals = new PatientVitals() { CreatedOn=DateTime.UtcNow, AppointmentId = model.AppointmentId,  BMI = model.BMI, DiastolicBP1 = model.DiastolicBP1, HeartRate = model.HeartRate, Height = model.Height, SystolicBP1 = model.SystolicBP1, Temprature = model.Temprature, Weight = model.Weight,Appointment=Appointment };
               
                var result = _appDbContext.PatientVitals.Add(PatientVitals);
                if (Appointment.PatientVitals == null)
                {
                    Appointment.PatientVitals = PatientVitals;
                }
                Appointment.UpdatedOn = DateTime.UtcNow;
                Appointment.ModefiedBy = userName;
                _appDbContext.Appointments.Update(Appointment);
                _appDbContext.SaveChanges();
                if (result == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Something went wrong!" });
                }

                return StatusCode(201, new { result.Entity });
            }
            else
            {
                return ValidationProblem();
            }


        }
        [HttpPost]
        [Route("GetAppointmentVitals")]
        public async Task<ActionResult<PatientVitals>> GetAppointmentVitals([FromBody] AppointmentIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                return _appDbContext.PatientVitals.Where(pv => pv.AppointmentId == model.AppointmentId && pv.IsDeleted==false).FirstOrDefault();
                
            }
            else
            {
                return ValidationProblem();
            }


        }
        [HttpPost]
        [Route("UpdateAppointmentStatus")]
        public async Task<ActionResult<PatientVitals>> UpdateAppointmentStatus([FromBody] AppointmentIdViewModel model)
        {

            if (ModelState.IsValid)
            {
                var Appointment = _appDbContext.Appointments.Where(a => a.AppointmentId == model.AppointmentId).FirstOrDefault();
                if (Appointment == null) return BadRequest(new Response
                {
                    Status = "Error",
                    Message = "Wrong appointment Id!"
                });
                AppointmentStatus status = (AppointmentStatus)model.AppointmentStatus;
                Appointment.Status = status;
                var result = _appDbContext.Appointments.Update(Appointment);
                _appDbContext.SaveChanges();
                return StatusCode(201, new { result.Entity });
            }
            else
            {
                return ValidationProblem();
            }


        }
        private string UploadedFile(IFormFile document)
        {
            string uniqueFileName = null;

            if (document != null)
            {
                string uploadsFolder = System.IO.Path.Combine(webHostEnvironment.WebRootPath, "Files");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + document.FileName;
                string filePath = System.IO.Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    document.CopyTo(fileStream);
                }
            }
            return "Files/" + uniqueFileName;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateAppointmentDocuments")]
        public async Task<IActionResult> CreateAppointmentDocuments([FromForm]AppointmentDocumentViewModel appointmentDocument)
        {
            if (appointmentDocument == null)
            {
                return BadRequest("No documents found.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Appointment = _appDbContext.Appointments.Where(a => a.AppointmentId == appointmentDocument!.AppointmentId).FirstOrDefault();
            if (Appointment == null) return BadRequest(new Response
            {
                Status = "Error",
                Message = "Wrong appointment Id!"
            });

            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity.Name;
            foreach (var document in appointmentDocument.Documents)
            {
                var uniqueFileName = UploadedFile(document);
                AppointmentDocument Document = new AppointmentDocument{
                    Appointment = Appointment,
                    AppointmentId = appointmentDocument.AppointmentId,
                    CreatedBy = userName,
                    CreatedOn = DateTime.UtcNow,
                    DocumentName = document.FileName,
                    DocumentPath = uniqueFileName,
                    IsDeleted = false,
                     type = appointmentDocument.type,
                };
                
                await _appDbContext.AppointmentDocuments.AddAsync(Document);
                if (Appointment.Documents == null)
                {
                    Appointment.Documents = new List<AppointmentDocument>();
                }
                Appointment.Documents.Add(Document);
                Appointment.UpdatedOn = DateTime.UtcNow;
                Appointment.ModefiedBy = userName;
                _appDbContext.Appointments.Update(Appointment);
            }

            await _appDbContext.SaveChangesAsync();

            return Ok("Documents uploaded successfully.");
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("testFile")]
        public async Task<IActionResult> testFile([FromForm] IFormFile file,string appintmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok("Documents received successfully.");
        }
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("CreateAppointmentDocument")]
        //public async Task<IActionResult> CreateAppointmentDocument([FromForm] AppointmentDocumentViewModel appointmentDocument)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var Appointment = _appDbContext.Appointments.Where(a => a.AppointmentId == appointmentDocument!.AppointmentId).FirstOrDefault();
        //        if (Appointment == null) return BadRequest(new Response
        //        {
        //            Status = "Error",
        //            Message = "Wrong appointment Id!"
        //        });
        //        ClaimsPrincipal _user = HttpContext?.User!;
        //        var userName = _user.Identity.Name;
        //        var uniqueFileName = UploadedFile(appointmentDocument);
        //        AppointmentDocument Document = new AppointmentDocument
        //        {
        //            Appointment = Appointment,
        //            AppointmentId = appointmentDocument.AppointmentId,
        //            CreatedBy = userName,
        //            CreatedOn = DateTime.UtcNow,
        //            DocumentName = appointmentDocument.DocumentName,
        //            DocumentPath = uniqueFileName,
        //            IsDeleted = false,
        //            type = appointmentDocument.type,
        //        };

        //        await _appDbContext.AppointmentDocuments.AddAsync(Document);
        //        if (Appointment.Documents == null)
        //        {
        //            Appointment.Documents = new List<AppointmentDocument>();
        //        }
        //        Appointment.Documents.Add(Document);
        //        Appointment.UpdatedOn = DateTime.UtcNow;
        //        Appointment.ModefiedBy = userName;
        //        _appDbContext.Appointments.Update(Appointment);
            

        //    await _appDbContext.SaveChangesAsync();

        //    return Ok("Documents uploaded successfully.");
        //}

    }
}
