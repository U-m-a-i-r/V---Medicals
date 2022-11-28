using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.Services;

namespace V___Medicals.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SpecialitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        //private readonly SignInManager<User> _signInManager;

        public SpecialitiesController(
        ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            // _signInManager = signInManager;
        }
        [HttpGet]
        [Route("GetAllSpecialities")]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetTodoItems()
        {
            return await _appDbContext.Specialities.Where(s=>s.IsActive==true).ToListAsync();
        }
    }
}
