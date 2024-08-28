using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Interface;

namespace ShushrutEyeHospitalCRM.Services.Implementation
{
    public class RefractionService : IRefractionService
    {
        private readonly IRepositories<Refraction> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RefractionService(IRepositories<Refraction> repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<string> DeleteRefraction(int Id)
        {
            var res = await _repository.GetByIdAsync(Id);
            return await _repository.DeleteAsync(res);
        }

        public async Task<RefractionViewModel> GetRefractionById(int Id)
        {
            return _mapper.Map<RefractionViewModel>(await _repository.GetByIdAsync(Id));
        }

        public async Task<IEnumerable<RefractionViewModel>> GetRefractions()
        {
            return _mapper.Map<IEnumerable<RefractionViewModel>>(await _repository.GetAsync(x => x.Status != null));
        }

        public async Task<string> InsertRefraction(RefractionViewModel refractionViewModel)
        {
            var refraction = _mapper.Map<Refraction>(refractionViewModel);
            refraction.ApplicationUser!.NormalizedUserName = refraction.ApplicationUser.UserName.ToUpper();
            refraction.ApplicationUser!.NormalizedEmail = refraction.ApplicationUser.Email.ToUpper();
            refraction.ApplicationUser.LockoutEnabled = true;
            refraction.ApplicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(refraction.ApplicationUser, refractionViewModel.ApplicationUser!.Password);
            return await _repository.CreateAsync(refraction);
        }

        public async Task<string> UpdateRefraction(RefractionViewModel refractionViewModel)
        {
            return await _repository.UpdateAsync(_mapper.Map<Refraction>(refractionViewModel));
        }
    }
}
