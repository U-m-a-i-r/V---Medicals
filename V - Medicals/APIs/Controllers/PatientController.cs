using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class PatientsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _Patientrepository;

        public PatientsController(
            IConfiguration configuration,
            ILogger<PatientsController> logger,
            IPatientService Patientrepository)
        {
            _Patientrepository = Patientrepository;
            _configuration = configuration;
            _logger = logger;
        }
        [HttpGet]
        [Route("getAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _Patientrepository.GetAll();
            return Ok(patients);
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Insert([FromBody] PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createdPatient = await _Patientrepository.CreateAsync(model);
                return StatusCode(201, new { PatientId = createdPatient.PatientId });
            }
            else
            {
                return ValidationProblem();
            }
           /* if (model == null)
            {
                return BadRequest("Patient is null.");
            }
            var createdPatient = await _Patientrepository.CreateAsync(model);
            return StatusCode(201, new { PatientId = createdPatient.PatientId });*/
        }
    }
}
