using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Models.Wrapper;

namespace ShushrutEyeHospitalCRM.Services.Interface
{
    public interface IPatientService
    {
        Task<string> InsertPatient(PatientViewModel patientViewModel);
        Task<string> DeletePatient(PatientViewModel patientViewModel);
        Task<string> UpdatePatient(PatientViewModel patientViewModel);
        Task<string> UpdatePatientModel(Patient patient);
        Task<bool> IsMRDExists(int MRDNo);
        Task<IEnumerable<PatientViewModel>> GetPatients();
        Task<PagingResult<PatientViewModel>> GetPatientList(PatientFilter filter);
        Task<IEnumerable<PatientViewModel>> GetPatientListForCounseling();
        Task<IEnumerable<PatientViewModel>> GetDischargedPatientList();
        Task<PatientViewModel> GetPatientById(int? Id);
        Task<bool> GetPatientCount(int Id, string searchFrom);
        Task<Patient> GetPatientByIdToPatientModel(int? Id);
        Task<IEnumerable<PatientViewModel>> GetTodayPatients();
        Task<IEnumerable<PatientViewModel>> GetRemainingPatients();
        Task<IEnumerable<PatientViewModel>> GetTodayClosePatients();
		Task<OPDReportViewModel> OPDReports(int Id);
		Task<string> AddPatientHistory(PatientHistoryViewModel patientHistoryViewModel);
        Task<string> AddPatientCounseling(PatientCounselingViewModel patientCounselingViewModel);
        Task<string> UpdatePatientCounseling(PatientCounselingViewModel patientCounselingViewModel);
        Task<string> UpdatePatientHistory(PatientHistoryViewModel patientHistoryViewModel);
        Task<List<PatientHistoryViewModel>> GetPatientHistory(int Id);
        Task<string> AddAdvGlasses(PatientAdvGlassesViewModel patientAdvGlassesViewModel);
        Task<string> UpdateAdvGlasses(PatientAdvGlassesViewModel patientAdvGlassesViewModel);
        Task<PatientAdvGlassesViewModel> GetAdvGlasses(int Id);
        Task<int> CountVisit(int MRdNo,int Id);
        Task<string> AddExaminationDetail(List<PatientExaminationDetailViewModel> patientExaminationDetailViewModel);
        Task<string> UpdateExaminationDetail(List<PatientExaminationDetailViewModel> patientExaminationDetailViewModel);
        Task<List<PatientExaminationDetailViewModel>> GetExaminationDetail(int Id);

        Task<string> AddPatientDischarge(PatientDischargeViewModel patientViewModel);
        Task<string> UpdatePatientDischarge(PatientDischargeViewModel patientViewModel);
    }
}
