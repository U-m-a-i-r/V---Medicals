using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Services
{
    public interface IPatientService
    {

        Task<IEnumerable<Patient>> GetAll();
        Task<Patient> GetById(int Id);
        Task<Patient> CreateAsync(PatientViewModel Model, User user = null);
        Task DeleteAsync(int Id);
        Task<Patient> UpdateAsync(PatientViewModel model);
    }
}