namespace ShushrutEyeHospitalCRM.Models
{
    public class DashboardViewModel
    {
        public int? ActiveDoctors { get; set; }
        public int? TodayPatient { get; set; }
        public int? RemainingPatient { get; set; }
        public int? Receptionist{ get; set; }
        public IEnumerable<PatientViewModel>? PatientViewModel{ get; set; }
    }
}
