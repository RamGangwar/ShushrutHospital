namespace ShushrutEyeHospitalCRM.Models
{
    public class DoctorViewModel
    {
        public int? Id { get; set; }
        public string? Designation { get; set; }
        public int DepartmentId { get; set; }
        public string? Address { get; set; }
        public string? Specialist { get; set; }
        public string? ProfilePic { get; set; }
        public string? Signature { get; set; }
        public DateTime? DOB { get; set; }
        public string? Biography { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? UserId { get; set; }
        public bool? Status { get; set; }
        public ApplicationUserViewModel? ApplicationUser { get; set; }
        public virtual DepartmentViewModel? Department { get; set; }
    }
}
