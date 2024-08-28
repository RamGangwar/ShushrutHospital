using AutoMapper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Interface;

namespace ShushrutEyeHospitalCRM.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositories<Department> _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepositories<Department> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> DeleteDepartment(DepartmentViewModel departmentViewModel)
        {
            return await _repository.DeleteAsync(_mapper.Map<Department>(departmentViewModel));
        }

        public async Task<DepartmentViewModel> GetDepartmentById(int Id)
        {
            return _mapper.Map<DepartmentViewModel>(await _repository.GetAsync(x => x.Id == Id));
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetDepartments()
        {
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(await _repository.GetAsync(x => x.Status != null));
        }

        public async Task<string> InsertDepartment(DepartmentViewModel departmentViewModel)
        {
            return await _repository.CreateAsync(_mapper.Map<Department>(departmentViewModel));
        }

        public async Task<string> UpdateDepartment(DepartmentViewModel departmentViewModel)
        {
            return await _repository.UpdateAsync(_mapper.Map<Department>(departmentViewModel));
        }
    }
}
