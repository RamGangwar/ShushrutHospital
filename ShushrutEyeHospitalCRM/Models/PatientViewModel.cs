namespace ShushrutEyeHospitalCRM.Models
{
    public class PatientViewModel
    {
        public int? Id { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Problem { get; set; }
        public string? OtherProblem { get; set; }
        public string? BloodGroup { get; set; }
        public decimal? ConsultancyFee { get; set; }
        public DateTime? LastCheckupDate { get; set; }
        public string? UserId { get; set; }
        public int? DoctorId { get; set; }
        public DoctorViewModel? Doctor { get; set; }
        public int? CounslingId { get; set; }
        public bool IsCounseling { get; set; }
        public bool IsDischarged { get; set; }
        public int? RefractionId { get; set; }
        public int? ReceptionistId { get; set; }
        public bool? RefractionStatus { get; set; }
        public bool? CounselingStatus { get; set; }
        public bool? DoctorStatus { get; set; }
        public bool? IsRemaining { get; set; }
        public int MRDNo { get; set; }
        public int IsBySearch { get; set; }
        public string? RegDate { get; set; }
        public ApplicationUserViewModel? ApplicationUser { get; set; }
    }

    public class AddPatientViewModel
    {
        public IEnumerable<CommonEyeProblemViewModel>? Problems { get; set; }
        public IEnumerable<DoctorViewModel>? Doctors { get; set; }
        public IEnumerable<RefractionViewModel>? Refractions { get; set; }
    }
}
