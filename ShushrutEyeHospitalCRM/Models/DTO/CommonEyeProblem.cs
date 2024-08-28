using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class CommonEyeProblem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProblemId { get; set; }
        public string ProblemName { get; set; }
        public bool IsActive { get; set; }
    }
}
