using ShushrutEyeHospitalCRM.Models;

namespace ShushrutEyeHospitalCRM.Services.Interface
{
    public interface IRefractionService
    {
        Task<string> InsertRefraction(RefractionViewModel refractionViewModel);
        Task<string> DeleteRefraction(int Id);
        Task<string> UpdateRefraction(RefractionViewModel refractionViewModel);
        Task<IEnumerable<RefractionViewModel>> GetRefractions();
        Task<RefractionViewModel> GetRefractionById(int Id);
    }
}
