using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class PatientAdvGlasses
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public string? DistanceRE_Sph { get; set; }
        public string? DistanceRE_Cy { get; set; }
        public string? DistanceRE_Axis { get; set; }
        public string? DistanceRE_Prism { get; set; }
        public string? DistanceRE_VA { get; set; }
        public string? DistanceRE_NV { get; set; }
        public string? DistanceLE_Sph { get; set; }
        public string? DistanceLE_Cy { get; set; }
        public string? DistanceLE_Axis { get; set; }
        public string? DistanceLE_Prism { get; set; }
        public string? DistanceLE_VA { get; set; }
        public string? DistanceLE_NV { get; set; }
        public string? AddRE_Sph { get; set; }
        public string? AddRE_Cy { get; set; }
        public string? AddRE_Axis { get; set; }
        public string? AddRE_Prism { get; set; }
        public string? AddRE_VA { get; set; }
        public string? AddRE_NV { get; set; }
        public string? AddLE_Sph { get; set; }
        public string? AddLE_Cy { get; set; }
        public string? AddLE_Axis { get; set; }
        public string? AddLE_Prism { get; set; }
        public string? AddLE_VA { get; set; }
        public string? AddLE_NV { get; set; }
    }
}
