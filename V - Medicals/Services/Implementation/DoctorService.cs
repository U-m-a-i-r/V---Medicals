using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Services.Implementation
{
    public class DoctorService : BaseService, IDoctorService
    {
        private readonly UserManager<User> _userManager;
        public DoctorService(ApplicationDbContext dbContext, IHttpContextAccessor httpContext, UserManager<User> userManager) : base(dbContext, httpContext)
        {
            _userManager = userManager;
        }

        public Task<Doctor> CreateAsync(DoctorViewModel Model, User user = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return _dbContext.Doctors.Where(d => d.IsDeleted == false && d.Status==DoctorStatusTypes.Active).Include(d=>d.Speciality).ToList();
        }

        public Task<Doctor> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Doctor>> GetBySpecialityId(int SpecialityId)
        {
            return _dbContext.Doctors.Where(d => d.IsDeleted == false && d.Status == DoctorStatusTypes.Active && d.SpecialityId ==SpecialityId).Include(d => d.Speciality).ToList();
        }

        public async Task<IEnumerable<DoctorClinic>> GetDoctorClinics(int DoctorId)
        {
           return  _dbContext.DoctorClinics.Where(dc => dc.DoctorId == DoctorId).Include(d => d.Clinic).ThenInclude(c=>c.Availabilities).ThenInclude(a=>a.Slots).ToList();
           // throw new NotImplementedException();
        }
        public async Task<IEnumerable<Availability>> GetClinicAvailabilities(int ClinicId)
        {
            return _dbContext.Availabilities.Where(avl => avl.ClinicId == ClinicId && avl.Status==Constants.StatusTypes.Active).ToList();
            // throw new NotImplementedException();
        }
        public async Task<IEnumerable<Slot>> GetAvailableSlots(int AvailabilityId)
        {
            return _dbContext.Slots.Where(slot=> slot.AvailabilityId== AvailabilityId && slot.Status==SlotStatus.Available).ToList();
            // throw new NotImplementedException();
        }

        public Task<Doctor> UpdateAsync(DoctorViewModel model)
        {
            throw new NotImplementedException();
        }
    }     
}
