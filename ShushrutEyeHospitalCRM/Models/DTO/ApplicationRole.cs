using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShushrutEyeHospitalCRM.Models.DTO
{
    public class ApplicationRole : IdentityRole
    {
        [NotMapped]
        public int NoOfUsers { get; set; }
    }
}
