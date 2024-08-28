using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Models.Wrapper;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Services.Interface;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.Design;

namespace ShushrutEyeHospitalCRM.Services.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IRepositories<Patient> _repository;
        private readonly IRepositories<PatientHistory> _patientHistoryRepository;
        private readonly IRepositories<PatientCounseling> _patientcounslingRepository;
        private readonly IRepositories<PatientDischarge> _patientDischarge;
        private readonly IRepositories<PatientAdvGlasses> _patientAdvGlassesRepository;
        private readonly IRepositories<PatientExaminationDetail> _patientExaminationDetail;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PatientService(IRepositories<Patient> repository, IMapper mapper, UserManager<ApplicationUser> userManager, IRepositories<PatientHistory> patientHistoryRepository, IRepositories<PatientAdvGlasses> patientAdvGlassesRepository, IRepositories<PatientExaminationDetail> patientExaminationDetail, IRepositories<PatientCounseling> patientcounslingRepository, IRepositories<PatientDischarge> patientDischarge)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _patientHistoryRepository = patientHistoryRepository;
            _patientAdvGlassesRepository = patientAdvGlassesRepository;
            _patientExaminationDetail = patientExaminationDetail;
            _patientcounslingRepository = patientcounslingRepository;
            _patientDischarge = patientDischarge;
        }
        public async Task<string> DeletePatient(PatientViewModel patientViewModel)
        {
            return await _repository.DeleteAsync(_mapper.Map<Patient>(patientViewModel));
        }
        public async Task<bool> IsMRDExists(int MRDNo)
        {
            var res = await _repository.GetAsync(u => u.MRDNo == MRDNo);
            return res.Count() > 0 ? true : false;
        }

        public async Task<PatientViewModel> GetPatientById(int? Id)
        {
            var data = await _repository.GetAsync(x => x.Id == Id);
            var res = _mapper.Map<PatientViewModel>(data.FirstOrDefault());
            res.RegDate = string.Format("{0:dd/MM/yyyy hh:mm tt}", data.FirstOrDefault()!.CreatedDate);
            return res;
        }
        public async Task<int> CountVisit(int MRdNo, int Id)
        {
            var data = await _repository.GetAsync(x => x.MRDNo == MRdNo && x.Id <= Id);
            return data.Count();
        }

        public async Task<IEnumerable<PatientViewModel>> GetPatients()
        {
            return _mapper.Map<IEnumerable<PatientViewModel>>(await _repository.GetAsync(x => x.Status != null));
        }
        public async Task<PagingResult<PatientViewModel>> GetPatientList(PatientFilter filter)
        {

            IQueryable<Patient> query = await _repository.Get(x => x.Status == true);

            if (filter != null)
            {
                if (filter.MRDNo > 0)
                {
                    query = query.Where(c => c.MRDNo == filter.MRDNo);
                }
                if (filter.IsCounseling != null)
                {
                    query = query.Where(c => c.IsCounseling == filter.IsCounseling);
                }
                if (filter.IsDischarged != null)
                {
                    query = query.Where(c => c.IsDischarged == filter.IsDischarged);
                }
                if (filter.DoctorStatus != null)
                {
                    query = query.Where(c => c.DoctorStatus == filter.DoctorStatus);
                }
                if (filter.CounselingStatus != null)
                {
                    query = query.Where(c => c.CounselingStatus == filter.CounselingStatus);
                }
                if (filter.RefrectionStatus != null)
                {
                    query = query.Where(c => c.RefractionStatus == filter.RefrectionStatus);
                }
                if (!string.IsNullOrEmpty(filter.Gender))
                {
                    query = query.Where(c => c.Gender == filter.Gender);
                }
                if (!string.IsNullOrEmpty(filter.MobileNo))
                {
                    query = query.Where(c => c.ApplicationUser!.PhoneNumber == filter.MobileNo);
                }
                if (!string.IsNullOrEmpty(filter.PatientName))
                {
                    query = query.Where(c => c.ApplicationUser!.FirstName!.Contains(filter.PatientName));
                }
            }
            int totalRecord = query.Count();

            query = query.OrderByDescending(o => o.CreatedDate);
            if (filter!.Take.HasValue && filter.Take > 0)
            {
                query = Pagination(filter!, query);
            }
            var newquery = query.Select(d => new PatientViewModel
            {
                Id = d.Id,
                DOB = d.DOB,
                Gender = d.Gender,
                Address = d.Address,
                Problem = d.Problem,
                OtherProblem = d.OtherProblem,
                BloodGroup = d.BloodGroup,
                ConsultancyFee = d.ConsultancyFee,
                LastCheckupDate = d.LastCheckupDate,
                UserId = d.UserId,
                DoctorId = d.DoctorId,
                CounslingId = d.CounslingId,
                IsCounseling = d.IsCounseling,
                IsDischarged = d.IsDischarged,
                RefractionId = d.RefractionId,
                ReceptionistId = d.ReceptionistId,
                RefractionStatus = d.RefractionStatus,
                CounselingStatus = d.CounselingStatus,
                DoctorStatus = d.DoctorStatus,
                IsRemaining = d.IsRemaining,
                MRDNo = d.MRDNo,
                RegDate = d.CreatedDate.ToString(),
                ApplicationUser = new ApplicationUserViewModel
                {
                    FirstName = d.ApplicationUser!.FirstName,
                    LastName = d.ApplicationUser!.LastName,
                    PhoneNumber = d.ApplicationUser!.PhoneNumber,
                    Email = d.ApplicationUser!.Email,
                    UserName = d.ApplicationUser!.UserName,
                },
                Doctor = new DoctorViewModel
                {
                    Id = d.DoctorId,
                    Designation = d.Doctor!.Designation,
                    Specialist = d.Doctor!.Specialist,
                    Gender = d.Doctor!.Gender,
                    ApplicationUser = new ApplicationUserViewModel
                    {
                        FirstName = d.Doctor!.ApplicationUser!.FirstName,
                        LastName = d.Doctor!.ApplicationUser!.LastName,
                        PhoneNumber = d.Doctor!.ApplicationUser!.PhoneNumber,
                        Email = d.Doctor!.ApplicationUser!.Email,
                        UserName = d.Doctor!.ApplicationUser!.UserName,
                    }
                }


            });
            return new PagingResult<PatientViewModel>()
            {
                Data = newquery.ToList(),
                Total = totalRecord,
                ItemsPerPage = filter!.Take ?? 0,
            };
        }

        private IQueryable<Patient> Pagination(PatientFilter paginable, IQueryable<Patient> query)
        {
            if (paginable.Take.HasValue && paginable.Skip.HasValue)
            {
                return query.Skip(paginable.Skip.Value).Take(paginable.Take.Value);
            }
            return query;
        }
        public async Task<IEnumerable<PatientViewModel>> GetPatientListForCounseling()
        {
            return _mapper.Map<IEnumerable<PatientViewModel>>(await _repository.GetAsync(x => x.RefractionStatus == true && x.DoctorStatus == true && x.IsCounseling == true && x.CounselingStatus != true));
        }
        public async Task<IEnumerable<PatientViewModel>> GetDischargedPatientList()
        {
            return _mapper.Map<IEnumerable<PatientViewModel>>(await _repository.GetAsync(x => x.IsDischarged == true));
        }

        public async Task<IEnumerable<PatientViewModel>> GetTodayPatients()
        {
            return _mapper.Map<IEnumerable<PatientViewModel>>(await _repository.GetAsync(x => x.Status == true && x.CreatedDate!.Value.Date == DateTime.Now.Date));
        }
        public async Task<IEnumerable<PatientViewModel>> GetRemainingPatients()
        {
            return _mapper.Map<IEnumerable<PatientViewModel>>(await _repository.GetAsync(x => x.Status == true && x.CreatedDate!.Value.Date == DateTime.Now.Date && x.IsRemaining == true));
        }
        public async Task<IEnumerable<PatientViewModel>> GetTodayClosePatients()
        {
            return _mapper.Map<IEnumerable<PatientViewModel>>(await _repository.GetAsync(x => x.Status == true && x.CreatedDate!.Value.Date == DateTime.Now.Date && x.IsRemaining == false));
        }
        public async Task<string> InsertPatient(PatientViewModel patientViewModel)
        {
            var patient = _mapper.Map<Patient>(patientViewModel);
            patient.ApplicationUser!.NormalizedUserName = patient.ApplicationUser.UserName.ToUpper();
            patient.ApplicationUser!.NormalizedEmail = patient.ApplicationUser.Email.ToUpper();
            patient.ApplicationUser.LockoutEnabled = true;
            patient.ApplicationUser.PasswordHash = _userManager.PasswordHasher.HashPassword(patient.ApplicationUser, patientViewModel.ApplicationUser!.Password);
            return await _repository.CreateAsync(patient);
        }

        public async Task<string> UpdatePatient(PatientViewModel patientViewModel)
        {
            return await _repository.UpdateAsync(_mapper.Map<Patient>(patientViewModel));
        }
        public async Task<string> UpdatePatientModel(Patient patient)
        {
            return await _repository.UpdateAsync(patient);
        }
        public async Task<Patient> GetPatientByIdToPatientModel(int? Id)
        {
            var data = await _repository.GetAsync(x => x.Id == Id);
            return data.FirstOrDefault()!;
        }
        public async Task<OPDReportViewModel> OPDReports(int Id)
        {
            return _mapper.Map<OPDReportViewModel>(await _repository.GetAsync(x => x.Id == Id));
        }
        public async Task<string> AddPatientHistory(PatientHistoryViewModel patientHistoryViewModel)
        {
            return await _patientHistoryRepository.CreateAsync(_mapper.Map<PatientHistory>(patientHistoryViewModel));
        }

        public async Task<string> UpdatePatientHistory(PatientHistoryViewModel patientHistoryViewModel)
        {
            var history = await _patientHistoryRepository.GetAsync(x => x.PatientId == patientHistoryViewModel.PatientId);
            var patienthistory = history.FirstOrDefault();
            patienthistory.Prescriptions = patientHistoryViewModel.Prescriptions;
            patienthistory.DoctorAdvice = patientHistoryViewModel.DoctorAdvice;
            patienthistory.DoctorName = patientHistoryViewModel.DoctorName;
            patienthistory.Diagnosis = patientHistoryViewModel.Diagnosis;
            return await _patientHistoryRepository.UpdateAsync(patienthistory);
        }
        public async Task<List<PatientHistoryViewModel>> GetPatientHistory(int Id)
        {
            return _mapper.Map<List<PatientHistoryViewModel>>(await _patientHistoryRepository.GetAsync(x => x.PatientId == Id));
        }

        public async Task<string> AddAdvGlasses(PatientAdvGlassesViewModel patientAdvGlassesViewModel)
        {
            return await _patientAdvGlassesRepository.CreateAsync(_mapper.Map<PatientAdvGlasses>(patientAdvGlassesViewModel));
        }

        public async Task<string> UpdateAdvGlasses(PatientAdvGlassesViewModel patientAdvGlassesViewModel)
        {
            return await _patientAdvGlassesRepository.UpdateAsync(_mapper.Map<PatientAdvGlasses>(patientAdvGlassesViewModel));
        }

        public async Task<PatientAdvGlassesViewModel> GetAdvGlasses(int Id)
        {
            var res = await _patientAdvGlassesRepository.GetAsync(x => x.PatientId == Id);
            return _mapper.Map<PatientAdvGlassesViewModel>(res.FirstOrDefault());
        }

        public async Task<string> AddExaminationDetail(List<PatientExaminationDetailViewModel> patientExaminationDetailViewModel)
        {
            var result = true;
            foreach (var item in patientExaminationDetailViewModel)
            {
                var res = await _patientExaminationDetail.CreateAsync(_mapper.Map<PatientExaminationDetail>(item));
            }
            return result.ToString();
        }

        public async Task<string> UpdateExaminationDetail(List<PatientExaminationDetailViewModel> patientExaminationDetailViewModel)
        {
            var result = true;
            foreach (var item in patientExaminationDetailViewModel)
            {
                var res = await _patientExaminationDetail.UpdateAsync(_mapper.Map<PatientExaminationDetail>(item));
            }
            return result.ToString();
        }

        public async Task<List<PatientExaminationDetailViewModel>> GetExaminationDetail(int Id)
        {
            return _mapper.Map<List<PatientExaminationDetailViewModel>>(await _patientExaminationDetail.GetAsync(x => x.PatientId == Id));
        }

        public async Task<string> AddPatientCounseling(PatientCounselingViewModel patientCounselingViewModel)
        {
            var counseling = _mapper.Map<PatientCounseling>(patientCounselingViewModel);
            var time = DateTime.Now.TimeOfDay;
            counseling.SurgeryDateTime = patientCounselingViewModel.SurgeryDateTime!.Value + time;
            return await _patientcounslingRepository.CreateAsync(counseling);
        }

        public async Task<string> UpdatePatientCounseling(PatientCounselingViewModel model)
        {
            var history = await _patientcounslingRepository.GetAsync(x => x.PatientId == model.PatientId);
            var patienthistory = history.FirstOrDefault();
            if (patienthistory != null)
            {
                patienthistory!.MRNO = model.MRNO;
                patienthistory!.Diagnosis = model.Diagnosis;
                patienthistory!.OperatedEye = model.OperatedEye;
                patienthistory!.ProcedureName = model.ProcedureName;
                patienthistory!.PackageName = model.PackageName;
                patienthistory!.AdditionalProcedure = model.AdditionalProcedure;
                patienthistory!.AnesthesiaType = model.AnesthesiaType;
                patienthistory!.PatientOrParty = model.PatientOrParty;
                patienthistory!.Remarks = model.Remarks;
                patienthistory!.ApproxCharge = model.ApproxCharge;
                patienthistory!.DoctorName = model.DoctorName;
                patienthistory!.CounsellorName = model.CounsellorName;
                patienthistory!.Status = model.Status;
                patienthistory!.BookingDate = model.BookingDate;
                patienthistory!.SurgeryDateTime = model.SurgeryDateTime;

                return await _patientcounslingRepository.UpdateAsync(patienthistory);
            }
            else
            {
                return "Invalid Patient";
            }
        }

        public async Task<string> AddPatientDischarge(PatientDischargeViewModel patientCounselingViewModel)
        {
            var counseling = _mapper.Map<PatientDischarge>(patientCounselingViewModel);
            return await _patientDischarge.CreateAsync(counseling);
        }

        public async Task<string> UpdatePatientDischarge(PatientDischargeViewModel model)
        {
            var history = await _patientDischarge.GetAsync(x => x.PatientId == model.PatientId);
            var patientdisc = history.LastOrDefault();
            if (patientdisc != null)
            {
                patientdisc.DischargeDetail = model.DischargeDetail;
                return await _patientDischarge.UpdateAsync(patientdisc);
            }
            else
            {
                return "Invalid Patient";
            }
        }

        public async Task<bool> GetPatientCount(int Id, string searchFrom)
        {
            var data = new List<Patient>();
            if (searchFrom.ToLower() == "reception")
            {
                data = (await _repository.GetAsync(x => x.ReceptionistId == Id)).ToList();
            }
            else if (searchFrom.ToLower() == "refrection")
            {
                data = (await _repository.GetAsync(x => x.RefractionId == Id)).ToList();
            }
            else if (searchFrom.ToLower() == "doctor")
            {
                data = (await _repository.GetAsync(x => x.DoctorId == Id)).ToList();
            }
            else if (searchFrom.ToLower() == "counseling")
            {
                data = (await _repository.GetAsync(x => x.CounslingId == Id)).ToList();
            }
            return data.Count > 0 ? true : false;
        }
    }
}
