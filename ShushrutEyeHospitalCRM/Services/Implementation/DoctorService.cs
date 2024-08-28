using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Interface;

namespace ShushrutEyeHospitalCRM.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepositories<Doctor> _repository;
        private readonly IRepositories<Department> _departmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public DoctorService(IRepositories<Doctor> repository, IMapper mapper, IRepositories<Department> departmentRepository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _userManager = userManager;
        }

        public async Task<string> DeleteDoctor(int Id)
        {
            var doc = await _repository.GetByIdAsync(Id);
            return await _repository.DeleteAsync(doc);
        }

        public async Task<DoctorViewModel> GetDoctorById(int Id)
        {
            return _mapper.Map<DoctorViewModel>(await _repository.GetByIdAsync(Id));
        }

        public async Task<Doctor> DoctorById(int Id)
        {
            return await _repository.GetByIdAsync(Id);
        }
        public async Task<IEnumerable<DoctorViewModel>> GetDoctors()
        {
            return _mapper.Map<IEnumerable<DoctorViewModel>>(await _repository.GetAsync(x => x.Status != null));
        }

        public async Task<string> InsertDoctor(DoctorViewModel doctorViewModel)
        {
            var doctor = _mapper.Map<Doctor>(doctorViewModel);
            doctor.ApplicationUser!.NormalizedUserName = doctor.ApplicationUser.UserName.ToUpper();
            doctor.ApplicationUser!.NormalizedEmail = doctor.ApplicationUser.Email.ToUpper();
            doctor.ApplicationUser.LockoutEnabled = true;
            doctor.ApplicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(doctor.ApplicationUser, doctorViewModel.ApplicationUser!.Password);
            return await _repository.CreateAsync(doctor);
        }

        public async Task<string> UpdateDoctor(Doctor doctorViewModel)
        {
            //var doctor = _mapper.Map<Doctor>(doctorViewModel);
            var res = await _repository.UpdateAsync(doctorViewModel);
            return res;

        }
        public async Task<IEnumerable<DepartmentViewModel>> GetActiveDepartments()
        {
            return _mapper.Map<IEnumerable<DepartmentViewModel>>(await _departmentRepository.GetAsync(x => x.Status == true));
        }
        public async Task<IEnumerable<DoctorViewModel>> GetActiveDoctors()
        {
            return _mapper.Map<IEnumerable<DoctorViewModel>>(await _repository.GetAsync(x => x.Status == true));
        }
    }
}
