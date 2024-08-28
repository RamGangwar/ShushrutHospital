using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class Medicines
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public bool IsActive { get; set; }
    }
}
