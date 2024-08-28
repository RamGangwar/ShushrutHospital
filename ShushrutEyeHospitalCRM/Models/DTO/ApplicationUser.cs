using Microsoft.AspNetCore.Identity;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual List<Doctor>? Doctors { get; set; }
        public virtual List<Patient>? Patients { get; set; }
        public virtual List<Receptionist>? Receptionists { get; set; }
        public virtual List<Counsling>? Counslings { get; set; }
        public virtual List<Refraction>? Refractions { get; set; }
    }
}
