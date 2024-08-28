using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Numerics;

namespace ShushrutEyeHospitalCRM.Services.Implementation
{
    public class CounslingService : ICounslingService
    {
        private readonly IRepositories<Counsling> _repository;
        private readonly IRepositories<PatientCounseling> _counselingrepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CounslingService(IRepositories<Counsling> repository, IMapper mapper, UserManager<ApplicationUser> userManager, IRepositories<PatientCounseling> counselingrepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _counselingrepository = counselingrepository;
        }
        public async Task<string> DeleteCounsling(int Id)
        {
            var res = await _repository.GetByIdAsync(Id);
            return await _repository.DeleteAsync(res);
        }

        public async Task<IEnumerable<PatientCounselingViewModel>> GetCounsling()
        {
            var couselinglist = await _counselingrepository.GetAsync(x => x.Status == true);
            return _mapper.Map<IEnumerable<PatientCounselingViewModel>>(couselinglist);
        }

        public async Task<CounslingViewModel> GetCounslingById(int Id)
        {
            return _mapper.Map<CounslingViewModel>(await _repository.GetAsync(x => x.Id == Id));
        }

        public async Task<string> InsertCounsling(CounslingViewModel CounslingViewModel)
        {
            var Counsling = _mapper.Map<Counsling>(CounslingViewModel);
            Counsling.ApplicationUser!.NormalizedUserName = Counsling.ApplicationUser.UserName.ToUpper();
            Counsling.ApplicationUser!.NormalizedEmail = Counsling.ApplicationUser.Email.ToUpper();
            Counsling.ApplicationUser.LockoutEnabled = true;
            Counsling.ApplicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(Counsling.ApplicationUser, CounslingViewModel.ApplicationUser!.Password);
            return await _repository.CreateAsync(Counsling);
        }

        public async Task<string> UpdateCounsling(CounslingViewModel CounslingViewModel)
        {
            return await _repository.UpdateAsync(_mapper.Map<Counsling>(CounslingViewModel));
        }
        public async Task<IEnumerable<CounslingViewModel>> GetActiveCounsling()
        {
            return _mapper.Map<IEnumerable<CounslingViewModel>>(await _repository.GetAsync(x => x.Status == true));
        }
    }
}
