using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Resources;
using ShushrutEyeHospitalCRM.Services.Interface;

namespace ShushrutEyeHospitalCRM.Controllers
{
    [Authorize(Roles = CommonHelper.Admin)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _departmentService.GetDepartments());
        }
        public IActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentViewModel departmentViewModel)
        {
            return ModelState.IsValid switch
            {
                true => Json(new { message = await _departmentService.InsertDepartment(departmentViewModel), status = true }),
                _ => Json(new { message = CommonResource.BlankField, status = false })
            };
        }
    }
}
