using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class PatientCounseling
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
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
        public virtual Patient? Patient { get; set; }
    }
}
