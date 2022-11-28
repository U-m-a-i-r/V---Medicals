using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<IEnumerable<DoctorClinic>> GetDoctorClinics(int DoctorId);
        Task<Doctor> GetById(int Id);
        Task<IEnumerable<Doctor>> GetBySpecialityId(int SpecialityId);
        Task<Doctor> CreateAsync(DoctorViewModel Model, User user = null);
        Task DeleteAsync(int Id);
        Task<Doctor> UpdateAsync(DoctorViewModel model);
    }
}
