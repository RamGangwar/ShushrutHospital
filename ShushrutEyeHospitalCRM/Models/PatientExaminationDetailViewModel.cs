namespace ShushrutEyeHospitalCRM.Models
{
    public class PatientExaminationDetailViewModel
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public string? FindingName { get; set; }
        public string? RightEye { get; set; }
        public string? LeftEye { get; set; }
    }
}
