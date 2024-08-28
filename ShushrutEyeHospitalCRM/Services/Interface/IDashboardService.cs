using ShushrutEyeHospitalCRM.Models;

namespace ShushrutEyeHospitalCRM.Services.Interface
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
        Task<DoctorDashboardViewModel> GetDoctorDashboardDataAsync(int doctorId);
        Task<ReceptionDashboardViewModel> GetReceptionDashboardDataAsync(int receptionId);
        Task<CounslingDashboardViewModel> GetCounslingDashboardDataAsync(int counslingId);
        Task<RefractionDashboardViewModel> GetRefractionDashboardDataAsync(int refractionId);
    }
}
