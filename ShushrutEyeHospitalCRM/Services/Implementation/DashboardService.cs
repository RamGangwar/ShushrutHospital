using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Services.Interface;

namespace ShushrutEyeHospitalCRM.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IReceptionService _receptionService;

        public DashboardService(IPatientService patientService, IDoctorService doctorService, IReceptionService receptionService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _receptionService = receptionService;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var activeDoctors = await _doctorService.GetActiveDoctors();
            var todayPatients = await _patientService.GetTodayPatients();
            var remainingPatients = await _patientService.GetRemainingPatients();
            var receptionist = await _receptionService.GetReceptionist();
            var remainingPatient = await _patientService.GetRemainingPatients();
            return new DashboardViewModel()
            {
                ActiveDoctors = activeDoctors.Count(),
                TodayPatient = todayPatients.Count(),
                RemainingPatient = remainingPatients.Count(),
                Receptionist = receptionist.Count(),
                PatientViewModel = remainingPatient
            };
        }
        public async Task<DoctorDashboardViewModel> GetDoctorDashboardDataAsync(int doctorId)
        {
            var totalPatients = await _patientService.GetPatients();
            var todayPatients = await _patientService.GetTodayPatients();
            var remainingPatients = await _patientService.GetRemainingPatients();
            var todayClosePatients = await _patientService.GetTodayClosePatients();
            return new DoctorDashboardViewModel()
            {
                TotalPatients = totalPatients.Where(x => x.DoctorId == doctorId).Count(),
                TodayPatients = todayPatients.Where(x => x.DoctorId == doctorId).Count(),
                RemainingPatient = remainingPatients.Where(x => x.DoctorId == doctorId).Count(),
                TodayClosePatients = todayClosePatients.Where(x => x.DoctorId == doctorId).Count(),
                PatientViewModel = totalPatients.Where(x => x.DoctorId == doctorId && x.DoctorStatus == false),
            };
        }
        public async Task<ReceptionDashboardViewModel> GetReceptionDashboardDataAsync(int receptionId)
        {
            var totalPatients = await _patientService.GetPatients();
            var todayPatients = await _patientService.GetTodayPatients();
            var remainingPatients = await _patientService.GetRemainingPatients();
            var todayClosePatients = await _patientService.GetTodayClosePatients();
            return new ReceptionDashboardViewModel()
            {
                TotalPatients = totalPatients.Where(x => x.ReceptionistId == receptionId).Count(),
                TodayPatients = todayPatients.Where(x => x.ReceptionistId == receptionId).Count(),
                RemainingPatient = remainingPatients.Where(x => x.ReceptionistId == receptionId).Count(),
                TodayClosePatients = todayClosePatients.Where(x => x.ReceptionistId == receptionId).Count(),
                PatientViewModel = totalPatients.Where(x => x.ReceptionistId == receptionId),
            };
        }
        public async Task<CounslingDashboardViewModel> GetCounslingDashboardDataAsync(int counslingId)
        {
            var totalPatients = await _patientService.GetPatients();
            var todayPatients = await _patientService.GetTodayPatients();
            var remainingPatients = await _patientService.GetRemainingPatients();
            var todayClosePatients = await _patientService.GetTodayClosePatients();
            return new CounslingDashboardViewModel()
            {
                TotalPatients = totalPatients.Where(x => x.CounslingId == counslingId).Count(),
                TodayPatients = todayPatients.Where(x => x.CounslingId == counslingId).Count(),
                RemainingPatient = remainingPatients.Where(x => x.CounslingId == counslingId).Count(),
                TodayClosePatients = todayClosePatients.Where(x => x.CounslingId == counslingId).Count(),
                PatientViewModel = totalPatients.Where(x => x.CounslingId == counslingId),
            };
        }
        public async Task<RefractionDashboardViewModel> GetRefractionDashboardDataAsync(int refractionId)
        {
            var totalPatients = await _patientService.GetPatients();
            var todayPatients = await _patientService.GetTodayPatients();
            var remainingPatients = await _patientService.GetRemainingPatients();
            var todayClosePatients = await _patientService.GetTodayClosePatients();
            return new RefractionDashboardViewModel()
            {
                TotalPatients = totalPatients.Where(x => x.RefractionId == refractionId).Count(),
                TodayPatients = todayPatients.Where(x => x.RefractionId == refractionId).Count(),
                RemainingPatient = remainingPatients.Where(x => x.RefractionId == refractionId).Count(),
                TodayClosePatients = todayClosePatients.Where(x => x.RefractionId == refractionId).Count(),
                PatientViewModel = totalPatients.Where(x => x.RefractionId == refractionId && x.RefractionStatus == false),
            };
        }
    }
}
