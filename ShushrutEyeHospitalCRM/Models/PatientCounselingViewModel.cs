using ShushrutEyeHospitalCRM.Models.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models
{
    public class PatientCounselingViewModel
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public string? MRNO { get; set; }
        public string? Diagnosis { get; set; }
        public string? OperatedEye { get; set; }
        public string? ProcedureName { get; set; }
        public string? PackageName { get; set; }
        public string? AdditionalProcedure { get; set; }
        public string? AnesthesiaType { get; set; }
        public string? PatientOrParty { get; set; }
        public string? Remarks { get; set; }
        public decimal ApproxCharge { get; set; }
        public string? DoctorName { get; set; }
        public string? CounsellorName { get; set; }
        public bool? Status { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? SurgeryDateTime { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public PatientViewModel? Patient { get; set; }
    }
}
