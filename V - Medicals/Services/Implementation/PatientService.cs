using Microsoft.AspNetCore.Identity;
using System;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Services.Implementation
{
    public class PatientService : BaseService, IPatientService
    {
        private readonly UserManager<User> _userManager;
        public PatientService(ApplicationDbContext dbContext, IHttpContextAccessor httpContext, UserManager<User> userManager) : base(dbContext, httpContext)
        {
            _userManager = userManager;
        }


        public async Task<Patient> CreateAsync(PatientRegisterModel Model, User user = null)
        {
            if (user == null)
            {
                var userEmail = User.GetUserEmail();
                user = await _userManager.FindByEmailAsync(userEmail);
            }
            //var model = _mapper.Map<Patient>(Model);
            Patient patient = new Patient();
            patient.Title = Model.Title;
            patient.FirstName = Model.FirstName;
            patient.MiddleName = Model.MiddleName;
            patient.LastName = Model.LastName;
            patient.Gender = Model.Gender;
            patient.DOB = Model.DOB;
            patient.Email = Model.Email;
            patient.PhoneNumber = Model.PhoneNumber;
            patient.CreatedOn = DateTime.Now;
            patient.UpdatedOn = DateTime.Now;
            patient.IsDeleted = false;
            patient.User = user;
            patient.UserId = user.Id;
            if (Model.Address != null)
            {
                patient.Address = new Address()
                {
                    AddressLine1 = Model.Address.AddressLine1,
                    AddressLine2 = Model.Address.AddressLine2,
                    City = Model.Address.City,
                    District = Model.Address.District,
                    Region = Model.Address.Region,
                    PostalCode = Model.Address.PostalCode,
                    DoctorId = null,
                };
            }
            var entity = await _dbContext.Patients.AddAsync(patient);
            _dbContext.SaveChanges();
            return entity.Entity;
        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            var userEmail = User.GetUserEmail();
            var _user = await _userManager.FindByEmailAsync(userEmail);
            if (_user == null)
            {
                throw new NotImplementedException("User is not Logged In!");
            }
            return _dbContext.Patients.Where(p => p.User.Id == _user.Id && p.IsDeleted == false).Select(p => new Patient
            {
                PatientId = p.PatientId,
                Title = p.Title,
                FirstName = p.FirstName,
                MiddleName = p.MiddleName,
                LastName = p.LastName,
                Gender = p.Gender,
                DOB = p.DOB,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                User = p.User,
                // User =p.User,
                Address = p.Address,
                CreatedOn = p.CreatedOn,
                UpdatedOn = p.UpdatedOn
            }).ToList();
        }

        public Task<Patient> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Patient> UpdateAsync(PatientRegisterModel model)
        {
            throw new NotImplementedException();
        }
    }
}