using Microsoft.AspNetCore.Mvc;

namespace ShushrutEyeHospitalCRM.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookAppointment()
        {
            return View();
        }
    }
}
