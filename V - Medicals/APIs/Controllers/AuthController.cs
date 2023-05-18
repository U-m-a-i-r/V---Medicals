﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using V___Medicals.Models;
using V___Medicals.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using V___Medicals.ValidationModels;
using V___Medicals.Constants;
using V___Medicals.Services;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;

namespace V___Medicals.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IPatientService _Patientrepository;
        //private readonly SignInManager<User> _signInManager;

        public AuthController(
            UserManager<User> userManager,
            IConfiguration configuration,
            ILogger<AuthController> logger,
            IPatientService Patientrepository,
            RoleManager<IdentityRole<string>> roleManager,
        // SignInManager<User> signInManager,
        ApplicationDbContext appDbContext)
        {
            _Patientrepository = Patientrepository;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
            // _signInManager = signInManager;
        }

        /*[HttpPost]
        [Route("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegister userModel)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.GetUserEmail();
                var LoggedInuser = await _userManager.FindByEmailAsync(userEmail);
                if (LoggedInuser == null)
                {
                    return NotFound();
                }
                if (LoggedInuser.RoleId != 4)
                {
                    return BadRequest(new Response { Status = "Error", Code = "INVALID_Authorization", Message = "Only admin can add a new user!" });
                }
                if (userModel.RoleId == 2)
                {
                    if (userModel.DoctorId == null)
                    {
                        return BadRequest(new Response { Status = "Error", Code = "", Message = "Please enter the correct doctor ID!" });

                    }
                    else
                    {
                        Doctor? doctor = _appDbContext.Doctors.Where(d => d.DoctorId == userModel.DoctorId).FirstOrDefault();
                        if (doctor == null)
                        {
                            return BadRequest(new Response { Status = "Error", Code = "", Message = "Please enter the correct doctor ID!" });
                        }
                        if (doctor.Id != null)
                        {
                            return BadRequest(new Response { Status = "Error", Code = "", Message = "User already registered for this doctor!" });
                        }
                        User usertoAdd = new()
                        {
                            RoleId = userModel.RoleId,
                            Name = userModel.UserDetails.Name,
                            Created = DateTime.UtcNow,
                            Updated = DateTime.UtcNow,
                            Email = userModel.UserDetails.Email.Trim(),
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = userModel.UserDetails.UserName.Trim(),
                            PhoneNumber = userModel.UserDetails.PhoneNumber.Trim(),
                            //Doctor = doctor,
                            IsActive = true,

                        };
                        var result = await _userManager.CreateAsync(usertoAdd, userModel.UserDetails.Password);
                        if (result.Succeeded)
                        {
                            result = await _userManager.AddToRoleAsync(usertoAdd, "Doctor");
                            if (result.Succeeded)
                            {
                                return Ok(new { Status = "Success", Message = "User has been registered successfully." });

                            }


                            return Ok(new { Status = "Success", Message = "User has been registered successfully." });
                        }
                        else
                        {
                            return BadRequest(result);
                        }
                    }

                }
                if (userModel.RoleId == 1)
                {
                    if (userModel.PatientId == null)
                    {
                        return BadRequest(new Response { Status = "Error", Code = "", Message = "Please enter the correct Patient ID!" });

                    }
                    else
                    {
                        Patient? patient = _appDbContext.Patients.Where(d => d.PatientId == userModel.PatientId).FirstOrDefault();
                        if (patient == null)
                        {
                            return BadRequest(new Response { Status = "Error", Code = "", Message = "Please enter the correct patient ID!" });
                        }
                        if (patient.User.Id != null)
                        {
                            return BadRequest(new Response { Status = "Error", Code = "", Message = "User already registered for this patient!" });
                        }
                        User usertoAdd = new()
                        {
                            RoleId = userModel.RoleId,
                            Name = userModel.UserDetails.Name,
                            Created = DateTime.UtcNow,
                            Updated = DateTime.UtcNow,
                            Email = userModel.UserDetails.Email.Trim(),
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = userModel.UserDetails.UserName.Trim(),
                            PhoneNumber = userModel.UserDetails.PhoneNumber.Trim(),
                            //Patients = (ICollection<Patient>)patient,
                            IsActive = true,

                        };
                        var result = await _userManager.CreateAsync(usertoAdd, userModel.UserDetails.Password);
                        if (result.Succeeded)
                        {
                            result = await _userManager.AddToRoleAsync(usertoAdd, "Patient");
                            if (result.Succeeded)
                            {
                                return Ok(new { Status = "Success", Message = "User has been registered successfully." });

                            }


                            return Ok(new { Status = "Success", Message = "User has been registered successfully." });
                        }
                        else
                        {
                            return BadRequest(result);
                        }
                    }

                }
                if (userModel.RoleId == 4)
                {
                    User usertoAdd = new()
                    {
                        RoleId = userModel.RoleId,
                        Name = userModel.UserDetails.Name,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow,
                        Email = userModel.UserDetails.Email.Trim(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = userModel.UserDetails.UserName.Trim(),
                        PhoneNumber = userModel.UserDetails.PhoneNumber.Trim(),
                        IsActive = true,

                    };
                    var result = await _userManager.CreateAsync(usertoAdd, userModel.UserDetails.Password);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddToRoleAsync(usertoAdd, "Admin");
                        if (result.Succeeded)
                        {
                            return Ok(new { Status = "Success", Message = "User has been registered successfully." });

                        }


                        return Ok(new { Status = "Success", Message = "User has been registered successfully." });
                    }
                    else
                    {
                        return BadRequest(result);
                    }

                }
            }
            return ValidationProblem();

        }*/

        [AllowAnonymous]
        [HttpPost]
        [Route("checkemail")]
        public async Task<IActionResult> CheckEmail([FromBody] EmailViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userByEmail = _appDbContext.Users.Where(predicate => predicate.Email == model.Email).FirstOrDefault();
               // var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail == null)
                {
                    return Ok(new Response { Status="Success", Message="Email is available"});
                }
                else
                {
                    return Ok(new Response { Status = "Error", Message = "Email already Registered" });
                }
            }
            else
            {
                return ValidationProblem();
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {

            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    //ModelState.AddModelError(nameof(UserRegisterModel.Email), "Email already Registered");
                    return BadRequest(new Response { Status = "Error", Message = "Email already Registered" });
                }

                var userByName = await _userManager.FindByNameAsync(model.UserName);
                if (userByName != null)
                {
                    //ModelState.AddModelError(nameof(UserRegisterModel.UserName), "Username already registed.");
                    // return ValidationProblem();
                    return BadRequest(new Response { Status = "Error", Message = "Username already Registered" });
                }
                
                User user = new()
                { 
                    Name = model.Name!,
                    Email = model.Email!.Trim(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName.Trim(),
                    PhoneNumber = model.PhoneNumber!.Trim(),
                    IsActive = true
                };


                var userResult = await _userManager.CreateAsync(user, model.Password);
                if (userResult.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Constants.Constants.ROLE_PATIENT))
                    {
                        IdentityResult result3 = await _roleManager.CreateAsync(new IdentityRole(Constants.Constants.ROLE_PATIENT));
                    }

                    // IdentityResult result3 = await _roleManager.CreateAsync(new IdentityRole(FYP_VMedicals.Constants.Constants.ROLE_PATIENT));
                    //var roleResult = await RoleManager<>.CreateAsync(new IdentityRole("Admin"));
                   var  result = await _userManager.AddToRoleAsync(user, Constants.Constants.ROLE_PATIENT);
                    //MaintainRecord maintainRecord = new MaintainRecord();
                    //maintainRecord.Created = DateTime.Now;
                    //maintainRecord.UserName = user.Name;
                    //var maintainRecordResult = await _appDbContext.MaintainRecords.AddAsync(maintainRecord);
                    user.CreatedOn = DateTime.UtcNow;
                    user.CreatedBy = user.Name;
                    var updateUserResult = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var currentUser = HttpContext.User;
                        var Model = new PatientViewModel()
                        {
                            FirstName = model.Name.Trim().Split(' ').First(),
                            LastName = model.Name.Trim().Split(' ').First(),
                            Email = model.Email.Trim(),
                            PhoneNumber = model.PhoneNumber.Trim(),

                        };
                        var result2 = await _Patientrepository.CreateAsync(Model, user);
                        _appDbContext.SaveChanges();
                        return Ok(new { Status = "Success", Message = "User & Patient has been registered successfully." });

                    }



                    _appDbContext.SaveChanges();
                    return Ok(new { Status = "Success", Message = "User has been registered successfully." });
                }
                else
                {
                    return BadRequest(userResult.Errors);
                }
            }

            else
            {
                return ValidationProblem();
            }

            // return Ok(new { Status = "Success", Message = "User has been registered successfully." } );
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    if (!user.IsActive)
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Code = "USER_DELETED", Message = "This user is deleted." });

                    var token = CreateToken(user);

                    user.Token = token;
                    user.FcmTokem = model.FcmToken;
                    //user.maintainRecord.Updated = DateTime.UtcNow;
                    var UserRole = await _userManager.GetRolesAsync(user);
                    await _userManager.UpdateAsync(user);
                    var currentUser = HttpContext.User;
                    if (UserRole.Contains(Constants.Constants.ROLE_DOCTOR))
                    {
                        var doctor = _appDbContext.Doctors.Where(d => d.Id == user.Id && d.IsDeleted == false && d.Status == DoctorStatusTypes.Active).FirstOrDefault();
                        if (doctor != null)
                        {
                            DoctorViewModel veiwDoctor = new DoctorViewModel()
                            {
                                AddressLine = doctor.AddressLine,
                                City = doctor.City,
                                Discription = doctor.Discription,
                                District = doctor.District,
                                DOB = doctor.DOB,
                                Documents = doctor.Documents,
                                Email = doctor.Email,
                                FirstName = doctor.FirstName,
                                Gender = doctor.Gender,
                                LastName = doctor.LastName,
                                MiddleName = doctor.MiddleName,
                                PhoneNumber = doctor.PhoneNumber,
                                PhysicalConsultancyCharges = doctor.PhysicalConsultancyCharges,
                                PhysicalConsultancyPercentage = doctor.PhysicalConsultancyPercentage,
                                PostalCode = doctor.PostalCode,
                                Qualification = doctor.Qualification,
                                SpecialityId = doctor.SpecialityId,
                                Status = doctor.Status,
                                Title = doctor.Title,

                                VideoConsultancyCharges = doctor.VideoConsultancyCharges,
                                VideoConsultancyPercentage = doctor.VideoConsultancyPercentage


                            };

                            return Ok(new
                            {
                                Token = token,
                                Role = UserRole,
                                Name = user.Name,
                                UserId = user.Id,
                                DoctorId = doctor.DoctorId,
                                Doctor = doctor
                            });

                        }
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Code = "INVALID_CREDENTIALS", Message = "User is not active!" });
                    }
                    else if (UserRole.Contains(Constants.Constants.ROLE_PATIENT))
                    {
                        var patient = _appDbContext.Patients.Where(p => p.UserId == user.Id && p.IsDeleted == false).FirstOrDefault();
                        if (patient != null)
                        {
                            PatientViewModel viewPatient = new PatientViewModel()
                            {
                                Title = patient.Title,
                                Address = new AddressModel() { AddressLine = patient.AddressLine, City = patient.City, District = patient.District, PostalCode = patient.PostalCode },
                                CNIC = patient.CNIC,
                                DOB = patient.DOB,
                                Documents = patient.Documents,
                                Email = patient.Email,
                                FirstName = patient.FirstName,
                                Gender = patient.Gender,
                                LastName = patient.LastName,
                                MRNumber = patient.MRNumber,
                                MiddleName = patient.MiddleName,
                                PhoneNumber = patient.PhoneNumber

                            };
                            return Ok(new
                            {
                                Token = token,
                                Role = UserRole,
                                Name = user.Name,
                                UserId = user.Id,
                                PatientId = patient.PatientId,
                                Patient = viewPatient
                            });

                        }
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Code = "INVALID_CREDENTIALS", Message = "User is not active!" });
                    }
                    return Ok(new
                    {
                        Token = token,
                        Role = UserRole,
                        Name = user.Name,
                        UserId = user.Id
                    });
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Code = "INVALID_CREDENTIALS", Message = "Username or password is incorrect." });
            }
            else
            {
                return ValidationProblem();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("registerPatient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegistrationModelFromMobile model)
        {

            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    //ModelState.AddModelError(nameof(UserRegisterModel.Email), "Email already Registered");
                    return BadRequest(new Response { Status = "Error", Message = "Email already Registered" });
                }

                var userByName = await _userManager.FindByNameAsync(model.UserName);
                if (userByName != null)
                {
                    //ModelState.AddModelError(nameof(UserRegisterModel.UserName), "Username already registed.");
                    // return ValidationProblem();
                    return BadRequest(new Response { Status = "Error", Message = "Username already Registered" });
                }
                string? titleText = System.Enum.GetName(typeof(Title), model.Title!)!;

                User user = new()
                {
                    Name = titleText!+" "+model.FirstName!+" "+model.MiddleName!+ " " + model.LastName,
                    Email = model.Email!.Trim(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName.Trim(),
                    PhoneNumber = model.PhoneNumber!.Trim(),
                    IsActive = true
                };
                var userResult = await _userManager.CreateAsync(user, model.Password);
                if (userResult.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Constants.Constants.ROLE_PATIENT))
                    {
                        IdentityResult result3 = await _roleManager.CreateAsync(new IdentityRole(Constants.Constants.ROLE_PATIENT));
                    }
                    var result = await _userManager.AddToRoleAsync(user, Constants.Constants.ROLE_PATIENT);
                    user.CreatedBy = user.Name;
                    user.CreatedOn = DateTime.UtcNow;
                    var updateUserResult = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var currentUser = HttpContext.User;
                        var Model = new PatientViewModel()
                        {
                            Title = model.Title,
                            FirstName = model.FirstName,
                            MiddleName = model.MiddleName,
                             Address = model.Address,
                              CNIC = model.CNIC,
                               DOB = model.DOB,
                                 Gender = model.Gender,
                            LastName = model.LastName,
                            Email = model.Email.Trim(),
                            PhoneNumber = model.PhoneNumber.Trim(),

                        };
                        var result2 = await _Patientrepository.CreateAsync(Model, user);
                        _appDbContext.SaveChanges();
                        return Ok(new { Status = "Success", Message = "User & Patient has been registered successfully." });

                    }



                    _appDbContext.SaveChanges();
                    return Ok(new { Status = "Success", Message = "User has been registered successfully." });
                }
                else
                {
                    return BadRequest(userResult.Errors);
                }
            }

            else
            {
                return ValidationProblem();
            }

            // return Ok(new { Status = "Success", Message = "User has been registered successfully." } );
        }

        private String CreateToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
