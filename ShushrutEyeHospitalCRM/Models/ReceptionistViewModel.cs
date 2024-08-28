namespace ShushrutEyeHospitalCRM.Models
{
    public class ReceptionistViewModel
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? ProfilePic { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? UserId { get; set; }
        public bool? Status { get; set; }
        public ApplicationUserViewModel? ApplicationUser { get; set; }
    }
}
