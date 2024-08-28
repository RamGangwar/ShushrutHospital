namespace ShushrutEyeHospitalCRM.Models.Wrapper
{
    public class BaseFilter :Paginable
    {
        public string? SearchText { get; set; }
        public bool? IsActive { get; set; }
    }
}
