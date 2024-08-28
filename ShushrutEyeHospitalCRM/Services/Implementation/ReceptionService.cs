using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Numerics;

namespace ShushrutEyeHospitalCRM.Services.Implementation
{
    public class ReceptionService : IReceptionService
    {
        private readonly IRepositories<Receptionist> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ReceptionService(IRepositories<Receptionist> repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<string> DeleteReception(int Id)
        {
            var res = await _repository.GetByIdAsync(Id);
            return await _repository.DeleteAsync(res);
        }

        public async Task<IEnumerable<ReceptionistViewModel>> GetReceptionist()
        {
            return _mapper.Map<IEnumerable<ReceptionistViewModel>>(await _repository.GetAsync(x => x.Status != null));
        }

        public async Task<ReceptionistViewModel> GetReceptionistById(int Id)
        {
            return _mapper.Map<ReceptionistViewModel>(await _repository.GetByIdAsync(Id));
        }

        public async Task<string> InsertReception(ReceptionistViewModel receptionistViewModel)
        {
            var receptionist = _mapper.Map<Receptionist>(receptionistViewModel);
            receptionist.ApplicationUser!.NormalizedUserName = receptionist.ApplicationUser.UserName.ToUpper();
            receptionist.ApplicationUser!.NormalizedEmail = receptionist.ApplicationUser.Email.ToUpper();
            receptionist.ApplicationUser.LockoutEnabled = true;
            receptionist.ApplicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(receptionist.ApplicationUser, receptionistViewModel.ApplicationUser!.Password);
            return await _repository.CreateAsync(receptionist);
        }

        public async Task<string> UpdateReception(ReceptionistViewModel receptionistViewModel)
        {
            return await _repository.UpdateAsync(_mapper.Map<Receptionist>(receptionistViewModel));
        }
        public async Task<IEnumerable<ReceptionistViewModel>> GetActiveReceptionist()
        {
            return _mapper.Map<IEnumerable<ReceptionistViewModel>>(await _repository.GetAsync(x => x.Status == true));
        }
    }
}
