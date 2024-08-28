using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models
{
    public class PatientDischargeViewModel
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? ReceptionistId { get; set; }
        public string? DischargeDetail { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public PatientViewModel? Patient { get; set; }
    }
}
