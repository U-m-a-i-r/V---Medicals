﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V___Medicals.Services;
using V___Medicals.ValidationModels;

namespace V___Medicals.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DoctorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DoctorController> _logger;
        private readonly IDoctorService _Doctorrepository;

        public DoctorController(
            IConfiguration configuration,
            ILogger<DoctorController> logger,
            IDoctorService Doctorrepository)
        {
            _Doctorrepository = Doctorrepository;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAllDoctors")]
        public async Task<IActionResult> GetAllDoctor()
        {
            var doctors = await _Doctorrepository.GetAll();
            return Ok(doctors);
        }
        [HttpPost]
        [Route("getBySpeciality")]
        public async Task<IActionResult> GetDoctorsBySpeciality([FromBody] SpecialityIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doctors = await _Doctorrepository.GetBySpecialityId(model.SpecialityId);
                return Ok(doctors);
            }
            else
            {
               return ValidationProblem();
            }
            
        }

        [HttpPost]
        [Route("getDoctorClinics")]
        public async Task<IActionResult> GetDoctorsClinics([FromBody] DoctorIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                var clinics = await _Doctorrepository.GetDoctorClinics(model.DoctorId);
                return Ok(clinics);
            }
            else
            {
                return ValidationProblem();
            }

        }
        [HttpGet]
        [Route("getF2FClinics")]
        public async Task<IActionResult> GetF2FClinics()
        {
            var clinics = await _Doctorrepository.GetF2FClinics();
            return Ok(clinics);
        }
        [HttpPost]
        [Route("getClinicAvailabilities")]
        public async Task<IActionResult> GetClinicAvailabilities([FromBody] ClinicIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                var availabilities = await _Doctorrepository.GetClinicAvailabilities(model.ClinicId);
                return Ok(availabilities);
            }
            else
            {
                return ValidationProblem();
            }

        }
        [HttpPost]
        [Route("getAvailableSlots")]
        public async Task<IActionResult> GetAvailableSlots([FromBody] AvailabilityIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                var slots = await _Doctorrepository.GetAvailableSlots(model.AvailabilityId);
                return Ok(slots);
            }
            else
            {
                return ValidationProblem();
            }

        }
    }
}
