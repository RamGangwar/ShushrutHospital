using ShushrutEyeHospitalCRM.Models;

namespace ShushrutEyeHospitalCRM.Services.Interface
{
    public interface ICounslingService
    {
        Task<string> InsertCounsling(CounslingViewModel CounslingViewModel);
        Task<string> DeleteCounsling(int Id);
        Task<string> UpdateCounsling(CounslingViewModel CounslingViewModel);
        Task<IEnumerable<PatientCounselingViewModel>> GetCounsling();
        Task<CounslingViewModel> GetCounslingById(int Id);
        Task<IEnumerable<CounslingViewModel>> GetActiveCounsling();
    }
}
