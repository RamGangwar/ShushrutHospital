using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;

namespace ShushrutEyeHospitalCRM.Services.Interface
{
    public interface IDoctorService
    {
        Task<string> InsertDoctor(DoctorViewModel doctorViewModel);
        Task<string> DeleteDoctor(int Id);
        Task<string> UpdateDoctor(Doctor doctorViewModel);
        Task<IEnumerable<DoctorViewModel>> GetDoctors();
        Task<DoctorViewModel> GetDoctorById(int Id);
        Task<IEnumerable<DepartmentViewModel>> GetActiveDepartments();
        Task<IEnumerable<DoctorViewModel>> GetActiveDoctors();
        Task<Doctor> DoctorById(int Id);
    }
}
