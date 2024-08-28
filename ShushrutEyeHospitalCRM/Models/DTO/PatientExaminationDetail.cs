using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class PatientExaminationDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public string? FindingName { get; set; }
        public string? RightEye { get; set; }
        public string? LeftEye { get; set; }
    }
}
