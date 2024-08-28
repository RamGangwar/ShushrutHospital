using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class PatientDischarge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public int? ReceptionistId { get; set; }
        public string? DischargeDetail { get; set; }        
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public virtual Patient? Patient { get; set; }
    }
}
