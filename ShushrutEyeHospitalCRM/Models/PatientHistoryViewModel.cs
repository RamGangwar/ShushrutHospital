namespace ShushrutEyeHospitalCRM.Models
{
    public class PatientHistoryViewModel
    {
        public int? Id { get; set; }
        public int? PatientId { get; set; }
        public string? DoctorName { get; set; }
        public string? RefractionName { get; set; }
        public string? MethodName { get; set; }
        public string? IOPTime { get; set; }
        public string? IOPRE { get; set; }
        public string? IOPLE { get; set; }
        public string? PRE { get; set; }
        public string? PLE { get; set; }
        public string? Remarks { get; set; }
        public string? DistanceVisionREWithGlass { get; set; }
        public string? DistanceVisionREWithPinhole { get; set; }
        public string? DistanceVisionLEWithGlass { get; set; }
        public string? DistanceVisionLEWithPinhole { get; set; }
        public string? DistanceVisionREUnAided { get; set; }
        public string? DistanceVisionLEUnAided { get; set; }
        public string? NearVisionREUnAided { get; set; }
        public string? NearVisionLEUnAided { get; set; }
        public string? NearVisionREWithGlass { get; set; }
        public string? NearVisionREWithPinhole { get; set; }
        public string? NearVisionLEWithGlass { get; set; }
        public string? NearVisionLEWithPinhole { get; set; }
        public string? Prescriptions { get; set; }
        public string? DoctorAdvice { get; set; }
        public string? Diagnosis { get; set; }
        public bool IsCounseling { get; set; }
        public PatientAdvGlassesViewModel? PatientAdvGlasses { get; set; }
        public List<PatientExaminationDetailViewModel>? PatientExamination { get; set; }
    }
}
