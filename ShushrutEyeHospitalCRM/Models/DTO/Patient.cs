using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Problem { get; set; }
        public string? OtherProblem { get; set; }
        public string? BloodGroup { get; set; }
        public decimal? ConsultancyFee { get; set; }
        public bool? Status { get; set; } = true;
        public bool? IsRemaining { get; set; } = true;        
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastCheckupDate { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        [ForeignKey("Receptionist")]
        public int ReceptionistId { get; set; }
        [ForeignKey("Refraction")]
        public int RefractionId { get; set; }
        [ForeignKey("Counsling")]
        public int? CounslingId { get; set; }
        public int MRDNo { get; set; }
        public bool? RefractionStatus { get; set; }
        public bool? DoctorStatus { get; set; }
        public bool IsCounseling { get; set; }
        public bool? CounselingStatus { get; set; }
        public bool IsDischarged { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Receptionist? Receptionist { get; set; }
        public virtual Refraction? Refraction { get; set; }
        public virtual Counsling? Counsling{ get; set; }
        public virtual List<PatientHistory>? PatientHistories { get; set; }
    }
}
