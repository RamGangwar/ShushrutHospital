using AutoMapper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;

namespace ShushrutEyeHospitalCRM.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // ------ Application User Mapping -------
            CreateMap<ApplicationUserViewModel, ApplicationUser>().ReverseMap();

            // ------ Department Mapping -------
            CreateMap<DepartmentViewModel, Department>().ReverseMap();

            // ------ Doctor Mapping -------
            CreateMap<DoctorViewModel, Doctor>().ReverseMap();

            // ------ Patient Mapping -------
            CreateMap<PatientViewModel, Patient>().ReverseMap();

            // ------ Receptionist Mapping -------
            CreateMap<Receptionist, ReceptionistViewModel>().ReverseMap();

            // ------ Refactions Mapping -------
            CreateMap<RefractionViewModel, Refraction>().ReverseMap();

            // ------ DoctorViewModel to ApplicationViewModel Mapping -------
            CreateMap<DoctorViewModel, ApplicationUserViewModel>().ReverseMap();

            // ------ OPDReportViewModel to Patient  Mapping -------
            CreateMap<OPDReportViewModel, Patient>().ReverseMap();

            // ------ PatientHistoryViewModel to PatientHistory  Mapping -------
            CreateMap<PatientHistoryViewModel, PatientHistory>().ReverseMap();

            // ------ PatientAdvGlassesViewModel to PatientAdvGlasses  Mapping -------
            CreateMap<PatientAdvGlassesViewModel, PatientAdvGlasses>().ReverseMap();

            // ------ PatientExaminationDetailViewModel to PatientExaminationDetail  Mapping -------
            CreateMap<PatientExaminationDetailViewModel, PatientExaminationDetail>().ReverseMap();

            // ------ CounslingViewModel to CounslingDetail  Mapping -------
            CreateMap<CounslingViewModel, Counsling>().ReverseMap();

            // ------ PatientCounselingViewModel to PatientCounseling  Mapping -------
            CreateMap<PatientCounselingViewModel, PatientCounseling>().ReverseMap();

            // ------ PatientDischargeViewModel to PatientDischarge  Mapping -------
            CreateMap<PatientDischargeViewModel, PatientDischarge>().ReverseMap();

            CreateMap<CommonEyeProblemViewModel, CommonEyeProblem>().ReverseMap();

            CreateMap<MedicineViewModel, Medicines>().ReverseMap();

        }
    }
}
