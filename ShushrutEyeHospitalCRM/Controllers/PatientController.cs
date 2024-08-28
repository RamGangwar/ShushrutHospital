using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Resources;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Security.Claims;

namespace ShushrutEyeHospitalCRM.Controllers
{
    public class PatientController : Controller
    {
        private readonly IRepositories<PatientCounseling> _counseling;
        private readonly IRepositories<CommonEyeProblem> _problem;
        private readonly IRefractionService _refractionService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PatientController(IRepositories<PatientCounseling> counseling, IPatientService patientService, UserManager<ApplicationUser> userManager, IRefractionService refractionService, IMapper mapper, IDoctorService doctorService, IRepositories<CommonEyeProblem> problem)
        {
            _counseling = counseling;
            _patientService = patientService;
            _userManager = userManager;
            _refractionService = refractionService;
            _mapper = mapper;
            _doctorService = doctorService;
            _problem = problem;
        }

        [Authorize(Roles = CommonHelper.Reception)]
        public async Task<IActionResult> Index()
        {
            return View(await _patientService.GetPatients());
        }

        [Authorize(Roles = CommonHelper.Reception)]
        public async Task<IActionResult> AddPatient()
        {
            return View(new AddPatientViewModel() { Doctors = await _doctorService.GetActiveDoctors(), Refractions = await _refractionService.GetRefractions(), Problems = (_mapper.Map<IEnumerable<CommonEyeProblemViewModel>>(await _problem.GetAllAsync())) });
        }

        [Authorize(Roles = CommonHelper.Reception)]
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                Random r = new Random();
                var count = r.Next(0, 100000);

                var getbycount = await _patientService.IsMRDExists(count);
                if (getbycount)
                {
                    count = r.Next(0, 100000);
                }

                patientViewModel.ApplicationUser!.Email = patientViewModel.ApplicationUser.UserName = string.IsNullOrEmpty(patientViewModel.ApplicationUser.Email) switch
                {
                    true => string.Concat(string.Concat(patientViewModel.ApplicationUser.FirstName, patientViewModel.ApplicationUser.LastName![..1]), $"{new Random().Next(000, 999)}@samarthinfotech.in"),
                    _ => patientViewModel.ApplicationUser.Email
                };
                patientViewModel.IsRemaining = true;
                patientViewModel.RefractionStatus = false;
                patientViewModel.CounselingStatus = false;
                patientViewModel.DoctorStatus = false;
                patientViewModel.MRDNo = patientViewModel.IsBySearch > 0 ? patientViewModel.MRDNo : count;
                var userExist = await _userManager.FindByEmailAsync(patientViewModel.ApplicationUser!.Email);
                switch (userExist == null)
                {
                    case true:
                        patientViewModel.ReceptionistId = Convert.ToInt32(HttpContext.User.FindFirstValue(CommonHelper.Id));
                        return Json(new { message = await _patientService.InsertPatient(patientViewModel), status = true });
                    default:
                        return Json(new { message = CommonResource.AlreadyExist, status = false });
                }
            }
            return Json(new
            {
                message = CommonResource.CreateFailed,
                status = false
            });
        }

        [Authorize(Roles = CommonHelper.Refraction + "," + CommonHelper.Doctor)]
        public async Task<IActionResult> PatientCheckup(int Id)
        {
            ViewBag.PatientHistory = await _patientService.GetPatientHistory(Id);
            ViewBag.AdvGlasses = await _patientService.GetAdvGlasses(Id);
            ViewBag.ExaminationDetail = await _patientService.GetExaminationDetail(Id);
            var patient = await _patientService.GetPatientById(Id);
            var counvisit= await _patientService.CountVisit(patient.MRDNo,Id);
            ViewBag.VisitNo = counvisit.ToString();
            return View(patient);
        }

        [Authorize(Roles = CommonHelper.Refraction + "," + CommonHelper.Doctor)]
        [HttpPost]
        public async Task<IActionResult> PatientCheckup([FromBody] PatientHistoryViewModel patientHistoryViewModel)
        {
            var patient = await _patientService.GetPatientByIdToPatientModel(patientHistoryViewModel.PatientId);
            if (patient != null)
            {
                if (CommonHelper.Refraction == HttpContext.User.FindFirstValue(ClaimTypes.Role))
                {
                    patient.RefractionStatus = true;
                    patientHistoryViewModel.RefractionName = HttpContext.User.FindFirstValue(CommonHelper.Name);
                    await _patientService.UpdatePatientModel(patient!);
                    if (patientHistoryViewModel.PatientAdvGlasses != null)
                    {
                        await _patientService.AddAdvGlasses(patientHistoryViewModel.PatientAdvGlasses!);
                    }
                    return Json(new
                    {
                        message = await _patientService.AddPatientHistory(patientHistoryViewModel),
                        status = true
                    });
                }
                else
                {
                    patient.DoctorStatus = true;
                    patient.LastCheckupDate = DateTime.Now;
                    patient.IsRemaining = false;
                    patient.IsCounseling = patientHistoryViewModel.IsCounseling;
                    patientHistoryViewModel.DoctorName = HttpContext.User.FindFirstValue(CommonHelper.Name);
                    await _patientService.UpdatePatientModel(patient!);
                    if (patientHistoryViewModel.PatientExamination!.Count() > 0)
                    {
                        await _patientService.AddExaminationDetail(patientHistoryViewModel.PatientExamination!);
                    }
                    return Json(new
                    {
                        message = await _patientService.UpdatePatientHistory(patientHistoryViewModel),
                        status = true
                    });
                }
            }
            else
            {
                return Json(new { message = CommonResource.PatientNotExist, status = false });
            }
        }

        [Authorize(Roles = CommonHelper.Refraction + "," + CommonHelper.Doctor + "," + CommonHelper.Counsling)]
        public async Task<IActionResult> PatientList()
        {
            var filter = new BoolFilter();
            filter.DoctorStatus = true;
            filter.RefrectionStatus = true;
            //var response = await _patientService.GetPatientList(new PatientFilter { DoctorStatus = true, RefrectionStatus = true });
            return View(filter);
        }
        [Authorize(Roles = CommonHelper.Counsling)]
        public async Task<IActionResult> PatientListForCounseling()
        {
            var filter = new BoolFilter();
            filter.DoctorStatus = true;
            filter.IsCounseling = true;
            filter.CounselingStatus = false;
            filter.RefrectionStatus = true;
            return View("~/Views/Patient/PatientList.cshtml", filter);
        }


        [Authorize(Roles = CommonHelper.Counsling)]
        public async Task<IActionResult> PatientCounseling(int Id)
        {
            var patient = await _patientService.GetPatientById(Id);
            var doclist = await _doctorService.GetActiveDoctors();
            var tuple = Tuple.Create<PatientViewModel, IEnumerable<DoctorViewModel>>(patient, doclist);
            return View(tuple);
        }

        [Authorize(Roles = CommonHelper.Counsling)]
        [HttpPost]
        public async Task<IActionResult> PatientCounseling([FromBody] PatientCounselingViewModel patientCounselingViewModel)
        {
            var patient = await _patientService.GetPatientByIdToPatientModel(patientCounselingViewModel.PatientId);
            if (patient != null)
            {

                patient.CounselingStatus = true;
                patient.CounslingId = Convert.ToInt32(HttpContext.User.FindFirstValue(CommonHelper.Id));
                patientCounselingViewModel.CounsellorName = HttpContext.User.FindFirstValue(CommonHelper.Name);
                await _patientService.UpdatePatientModel(patient!);
                return Json(new
                {
                    message = await _patientService.AddPatientCounseling(patientCounselingViewModel),
                    status = true
                });


            }
            else
            {
                return Json(new { message = CommonResource.PatientNotExist, status = false });
            }
        }

        [Authorize(Roles = CommonHelper.Reception)]
        public async Task<IActionResult> DischargePatient(int Id)
        {
            var patient = await _patientService.GetPatientById(Id);
            var Counselinglist = await _counseling.GetAsync(u => u.PatientId == Id);
            var Counseling = _mapper.Map<PatientCounselingViewModel>(Counselinglist.LastOrDefault());
            var tuple = Tuple.Create<PatientViewModel, PatientCounselingViewModel>(patient, Counseling);
            return View(tuple);
        }

        [Authorize(Roles = CommonHelper.Reception)]
        [HttpPost]
        public async Task<IActionResult> DischargePatient([FromBody] PatientDischargeViewModel patientView)
        {
            var patient = await _patientService.GetPatientByIdToPatientModel(patientView.PatientId);
            if (patient != null)
            {
                patient.IsDischarged = true;
                patientView.ReceptionistId = Convert.ToInt32(HttpContext.User.FindFirstValue(CommonHelper.Id));
                await _patientService.UpdatePatientModel(patient!);
                return Json(new
                {
                    message = await _patientService.AddPatientDischarge(patientView),
                    status = true
                });
            }
            else
            {
                return Json(new { message = CommonResource.PatientNotExist, status = false });
            }
        }

        [Authorize(Roles = CommonHelper.Reception)]
        public async Task<IActionResult> DischargedPatientList()
        {
            var filter = new BoolFilter();
            filter.IsDischarged = true;
            return View("~/Views/Patient/PatientList.cshtml", filter);
        }

        [HttpPost]
        public async Task<IActionResult> GetPatientByFilter(DTParameters para, PatientFilter filter)
        {
            if (para.Order != null)
            {
                filter.SortOrder = Convert.ToString(para.Order[0].Dir).ToLower();
            }
            if (para.SortOrder != null)
            {
                if (para.SortOrder.Contains("."))
                {
                    filter.SortBy = para.SortOrder.Split('.')[1];
                }
                else
                {
                    filter.SortBy = para.SortOrder;
                }
            }

            filter.Take = para.Length;
            filter.Skip = para.Start;
            var result = await _patientService.GetPatientList(filter);
            return Json(new
            {
                draw = para.Draw,
                recordsFiltered = result.Total,
                recordsTotal = result.Total,
                data = result.Data
            });
        }

    }
}
