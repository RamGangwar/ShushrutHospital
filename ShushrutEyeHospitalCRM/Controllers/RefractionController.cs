using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Resources;
using ShushrutEyeHospitalCRM.Services.Implementation;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Security.Claims;

namespace ShushrutEyeHospitalCRM.Controllers
{
    public class RefractionController : Controller
    {
        private readonly IRefractionService _refractionService;
        private readonly IPatientService _patientService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDashboardService _dashboardService;

        public RefractionController(IRefractionService refractionService, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager, IDashboardService dashboardService, IPatientService patientService)
        {
            _refractionService = refractionService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _dashboardService = dashboardService;
            _patientService = patientService;
        }

        [Authorize(Roles = CommonHelper.Admin)]
        public async Task<IActionResult> Index()
        {
            return View(await _refractionService.GetRefractions());
        }

        [Authorize(Roles = CommonHelper.Admin)]
        public IActionResult AddRefraction()
        {
            return View();
        }

        [Authorize(Roles = CommonHelper.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddRefraction([FromBody] RefractionViewModel refractionViewModel)
        {
            switch (ModelState.IsValid)
            {
                case true:
                    var userExist = await _userManager.FindByEmailAsync(refractionViewModel.ApplicationUser!.Email);
                    switch (userExist == null)
                    {
                        case true:
                            if (refractionViewModel.ProfilePic != null)
                            {
                                string fileName = Path.GetRandomFileName() + ".png";
                                byte[] bytes = Convert.FromBase64String(refractionViewModel.ProfilePic!.Split("base64,")[1]);
                                MemoryStream memoryStream = new(bytes);
                                IFormFile formFile = new FormFile(memoryStream, 0, bytes.Length, fileName, fileName);
                                if (formFile != null)
                                {
                                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "refraction");
                                    (uploadsFolder != null ? new DirectoryInfo(uploadsFolder) : null)?.Create();
                                    string filePath = Path.Combine(uploadsFolder!, fileName);
                                    using var fileStream = new FileStream(filePath, FileMode.Create);
                                    formFile.CopyTo(fileStream);
                                }
                                refractionViewModel.ProfilePic = fileName;
                            }
                            await _refractionService.InsertRefraction(refractionViewModel);
                            var user = await _userManager.FindByEmailAsync(refractionViewModel.ApplicationUser!.Email);
                            var result = await _userManager.AddToRoleAsync(user, CommonHelper.Refraction);
                            return result.Succeeded switch { true => Json(new { message = CommonResource.CreateSuccess, status = true }), _ => Json(new { message = CommonResource.UserNotCreated, status = false }) };
                        default:
                            return Json(new { message = CommonResource.AlreadyExist, status = false });
                    }
                default:
                    return Json(new { message = CommonResource.BlankField, status = false });
            }
        }
        public async Task<IActionResult> Dashboard()
        {
            return View(await _dashboardService.GetRefractionDashboardDataAsync(Convert.ToInt32(HttpContext.User.FindFirstValue(CommonHelper.Id))));
        }

        [Authorize(Roles = CommonHelper.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteRefrection(int Id)
        {
            var check = await _patientService.GetPatientCount(Id, "Refrection");
            if (!check)
            {
                var receptionist = await _refractionService.GetRefractionById(Id);
                if (receptionist != null)
                {
                    var userExist = await _userManager.FindByEmailAsync(receptionist.ApplicationUser!.Email);
                    if (userExist != null)
                    {
                        await _userManager.DeleteAsync(userExist);
                        await _refractionService.DeleteRefraction(Id);

                        return Json(new { message = CommonResource.DeleteSuccess, status = true });
                    }
                    return Json(new { message = CommonResource.DeleteFailed, status = false });
                }
                return Json(new { message = CommonResource.DeleteFailed, status = false });
            }
            else
            {
                return Json(new { message = "Can not delete, patient detail updated by this refraction", status = false });
            }
        }
    }
}

