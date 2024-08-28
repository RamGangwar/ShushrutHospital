using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Security.Claims;

namespace ShushrutEyeHospitalCRM.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IRefractionService _refractionService;
        private readonly IRepositories<PatientCounseling> _counselingService;
        private readonly IRepositories<PatientDischarge> _dischargedPatient;
        private readonly IMapper _mapper;
        public ReportsController(IPatientService patientService, IDoctorService doctorService, IRefractionService refractionService, IRepositories<PatientCounseling> counselingService, IMapper mapper, IRepositories<PatientDischarge> dischargedPatient)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _refractionService = refractionService;
            _counselingService = counselingService;
            _mapper = mapper;
            _dischargedPatient = dischargedPatient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PatientDetails()
        {
            return View();
        }

        public IActionResult PatientDetailsPdf()
        {
            return View();
        }
        public async Task<IActionResult> GetPatientDetails(int id)
        {
            var model = await _patientService.OPDReports(id);

            return new ViewAsPdf("PatientDetailsPdf", model);
        }

        public async Task<IActionResult> CheckPatientDetails(int id)
        {
            string? msg = "";
            var model = await _patientService.GetPatientById(id);
            if (model != null)
            {
                msg = "Error";
                return Json(msg);
            }
            return Json(msg);
        }

        public IActionResult OpdReportPdf()
        {
            return View();
        }

        public async Task<IActionResult> OPDReports(int id)
        {
            var model = await _patientService.OPDReports(id);

            return new ViewAsPdf("OpdReportPdf", model);
        }

        public async Task<IActionResult> PatientCounseling(int Id)
        {
            var model = await _patientService.GetPatientById(Id);
            var counselingList = await _counselingService.GetAsync(u => u.PatientId == Id);
            var counseling = _mapper.Map<PatientCounselingViewModel>(counselingList.FirstOrDefault());
            var tuple = Tuple.Create<PatientViewModel, PatientCounselingViewModel>(model, counseling);

            //return View("~/Views/Reports/CounslingReport.cshtml", tuple);

            return new ViewAsPdf("~/Views/Reports/CounslingReport.cshtml", tuple)
            {
                FileName = "CounslingReport.pdf",
                PageMargins = { Left = 2, Bottom = 5, Right = 2, Top = 1 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                IsGrayScale = false,
            };

        }

        public async Task<IActionResult> PatientCounselingView(int Id)
        {
            var model = await _patientService.GetPatientById(Id);
            var counselingList = await _counselingService.GetAsync(u => u.PatientId == Id);
            var counseling = _mapper.Map<PatientCounselingViewModel>(counselingList.FirstOrDefault());
            var tuple = Tuple.Create<PatientViewModel, PatientCounselingViewModel>(model, counseling);

            return View("~/Views/Reports/CounslingReportView.cshtml", tuple);

          

        }

        public async Task<IActionResult> DischargedPatientView(int Id)
        {
            var model = await _patientService.GetPatientById(Id);
            var counselingList = await _counselingService.GetAsync(u => u.PatientId == Id);
            var counseling = _mapper.Map<PatientCounselingViewModel>(counselingList.LastOrDefault());
            var dischargeList = await _dischargedPatient.GetAsync(u => u.PatientId == Id);
            var discharge = _mapper.Map<PatientDischargeViewModel>(dischargeList.LastOrDefault());
            var tuple = Tuple.Create<PatientViewModel, PatientCounselingViewModel, PatientDischargeViewModel>(model, counseling, discharge);

            return View("~/Views/Reports/DischargeReportView.cshtml", tuple);

            

        }
        public async Task<IActionResult> DischargedPatient(int Id)
        {
            var model = await _patientService.GetPatientById(Id);
            var counselingList = await _counselingService.GetAsync(u => u.PatientId == Id);
            var counseling = _mapper.Map<PatientCounselingViewModel>(counselingList.LastOrDefault());
            var dischargeList = await _dischargedPatient.GetAsync(u => u.PatientId == Id);
            var discharge = _mapper.Map<PatientDischargeViewModel>(dischargeList.LastOrDefault());
            var tuple = Tuple.Create<PatientViewModel, PatientCounselingViewModel, PatientDischargeViewModel>(model, counseling, discharge);

            //return View("~/Views/Reports/DischargeReport.cshtml", tuple);

            return new ViewAsPdf("~/Views/Reports/DischargeReport.cshtml", tuple)
            {
                FileName = "DischargeReport.pdf",
                PageMargins = { Left = 2, Bottom = 5, Right = 2, Top = 1 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                IsGrayScale = false,
            };

        }
        public async Task<IActionResult> PatientCompleteDetailsView(int Id)
        {
            var model = await _patientService.GetPatientById(Id);
            var doctor = await _doctorService.GetDoctorById((int)model.DoctorId);
            model.Doctor = doctor;
            var history = await _patientService.GetPatientHistory(Id);
            foreach (var item in history)
            {
                item.PatientAdvGlasses = await _patientService.GetAdvGlasses((int)item.PatientId);
                item.PatientExamination = await _patientService.GetExaminationDetail((int)item.PatientId);
            }
            var tuple = Tuple.Create<PatientViewModel, IEnumerable<PatientHistoryViewModel>>(model, history.AsEnumerable());
            return View("~/Views/Reports/PatientCompleteReportView.cshtml", tuple);

            


        }
        public async Task<IActionResult> PatientCompleteDetails(int Id)
        {
            var model = await _patientService.GetPatientById(Id);
            var doctor = await _doctorService.GetDoctorById((int)model.DoctorId);
            model.Doctor = doctor;
            var history = await _patientService.GetPatientHistory(Id);
            foreach (var item in history)
            {
                item.PatientAdvGlasses = await _patientService.GetAdvGlasses((int)item.PatientId);
                item.PatientExamination = await _patientService.GetExaminationDetail((int)item.PatientId);
            }
            var tuple = Tuple.Create<PatientViewModel, IEnumerable<PatientHistoryViewModel>>(model, history.AsEnumerable());
            //return View("~/Views/Reports/PatientCompleteReport.cshtml", tuple);

            return new ViewAsPdf("~/Views/Reports/PatientCompleteReport.cshtml", tuple)
            {
                FileName = "PatientCheckUpDetail.pdf",
                PageMargins = { Left = 0, Bottom = 5, Right = 0, Top = 0 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                IsGrayScale = false,
            };


        }
        public async Task<IActionResult> PatientDetailsById(int Id)
        {
            var model = await _patientService.GetPatientById(Id);
            var doctorname = await _doctorService.GetDoctorById((int)model.DoctorId);
            var refractionname = await _refractionService.GetRefractionById((int)model.RefractionId);
            var tuple = Tuple.Create<PatientViewModel, string>(model, doctorname.ApplicationUser!.FirstName + " " + doctorname.ApplicationUser.LastName);
            return new ViewAsPdf("~/Views/Reports/PatientDetailReport.cshtml", tuple)
            {
                FileName = "PatientDetailReport.pdf",
                PageMargins = { Left = 0, Bottom = 10, Right = 0, Top = 0 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                IsGrayScale = false,


            };

        }
    }
}
