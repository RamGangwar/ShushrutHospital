using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class Receptionist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? ProfilePic { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public bool? Status { get; set; } = true;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual List<Patient>? Patient { get; set; }
    }
}
