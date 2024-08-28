namespace ShushrutEyeHospitalCRM.Helper
{
    public static class CommonHelper
    {
        public const string ConnectionString = "DefaultConnection";
        public const string Name = "Name";
        public const string Id = "Id";

        // Url's
        public const string LoginUrl = "/Authorization/Index";
        public const string AccessDeniedUrl = "/Authorization/AccessDenied";
        public const string DepartmentList = "/Department/Index";
        public const string DoctorList = "/Doctor/Index";

        // Roles
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
        public const string Reception = "Reception";
        public const string Refraction = "Refraction";
        public const string Counsling = "Counsling";

        // Dashboard Url's
        public const string AdminDashboardUrl = "/Home/Index";
        public const string DoctorDashboardUrl = "/Doctor/Dashboard";
        public const string PatientDashboardUrl = "/Patient/Dashboard";
        public const string ReceptionDashboardUrl = "/Reception/Dashboard";
        public const string RefractionDashboardUrl = "/Refraction/Dashboard";
        public const string CounslingDashboardUrl = "/Counsling/Dashboard";

        // Get Role Based Dashboard Url
        public static string GetDashboardUrl(string? role) => role switch
        {
            Admin => AdminDashboardUrl,
            Doctor => DoctorDashboardUrl,
            Patient => PatientDashboardUrl,
            Reception => ReceptionDashboardUrl,
            Refraction => RefractionDashboardUrl,
            Counsling => CounslingDashboardUrl,
            _ => AccessDeniedUrl,
        };
    }
}
