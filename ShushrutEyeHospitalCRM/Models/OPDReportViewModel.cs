namespace ShushrutEyeHospitalCRM.Models
{
	public class OPDReportViewModel
	{
		public int Id { get; set; }
		public DateTime? DOB { get; set; }
		public string? Gender { get; set; }
		public string? Address { get; set; }
		public string? Problem { get; set; }
		public string? BloodGroup { get; set; }
		public decimal? ConsultancyFee { get; set; }
		public DateTime? LastCheckupDate { get; set; }
		public string? UserId { get; set; }
		public int? DoctorId { get; set; }
		public int ReceptionistId { get; set; }
		//---
		public ApplicationUserViewModel? ApplicationUser { get; set; }
		//DOCTOR VIEW MODEL
		public DoctorViewModel? Doctor { get; set; }
		//DEPARTMENT VIEW MODEL
		public virtual DepartmentViewModel? Department { get; set; }
	
	}
}
