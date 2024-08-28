using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;

namespace ShushrutEyeHospitalCRM.Services.Interface
{
    public interface IDepartmentService
    {
        Task<string> InsertDepartment(DepartmentViewModel departmentViewModel);
        Task<string> DeleteDepartment(DepartmentViewModel departmentViewModel);
        Task<string> UpdateDepartment(DepartmentViewModel departmentViewModel);
        Task<IEnumerable<DepartmentViewModel>> GetDepartments();
        Task<DepartmentViewModel> GetDepartmentById(int Id);
    }
}
