using ShushrutEyeHospitalCRM.Models.Wrapper;

namespace ShushrutEyeHospitalCRM.Models
{
    public class PatientFilter : BaseFilter
    {
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public int MRDNo { get; set; }
        public bool? RefrectionStatus { get; set; }
        public bool? CounselingStatus { get; set; }
        public bool? IsCounseling { get; set; }
        public bool? DoctorStatus { get; set; }
        public bool? IsDischarged { get; set; }
    }
    public class BoolFilter
    {
        public bool? IsDischarged { get; set; }
        public bool? DoctorStatus { get; set; }
        public bool? IsCounseling { get; set; }
        public bool? CounselingStatus { get; set; }
        public bool? RefrectionStatus { get; set; }
    }
}
