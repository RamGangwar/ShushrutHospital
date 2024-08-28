﻿namespace ShushrutEyeHospitalCRM.Models
{
    public class DoctorDashboardViewModel
    {
        public int? TotalPatients { get; set; }
        public int? TodayPatients { get; set; }
        public int? RemainingPatient { get; set; }
        public int? TodayClosePatients { get; set; }
        public IEnumerable<PatientViewModel>? PatientViewModel { get; set; }
    }
}
