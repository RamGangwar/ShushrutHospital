using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Resources;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Security.Claims;

namespace ShushrutEyeHospitalCRM.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDashboardService _dashboardService;

        public DoctorController(IDoctorService doctorService, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager, IDashboardService dashboardService, IPatientService patientService)
        {
            _doctorService = doctorService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _dashboardService = dashboardService;
            _patientService = patientService;
        }

        [Authorize(Roles = CommonHelper.Admin)]
        public async Task<IActionResult> Index()
        {
            return View(await _doctorService.GetDoctors());
        }

        [Authorize(Roles = CommonHelper.Admin)]
        public async Task<IActionResult> AddDoctor()
        {
            return View(await _doctorService.GetActiveDepartments());
        }
        [Authorize(Roles = CommonHelper.Admin)]
        public async Task<IActionResult> EditDoctor(int Id)
        {
            var department = await _doctorService.GetActiveDepartments();
            var doctor = await _doctorService.GetDoctorById(Id);
            var tuple = Tuple.Create(doctor, department.ToList());
            return View(tuple);
        }

        [Authorize(Roles = CommonHelper.Admin)]
        [HttpPost]

        public async Task<IActionResult> EditDoctor([FromForm] DoctorViewModel Item1)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(Item1.ApplicationUser!.Email);
                if (userExist != null)
                {
                    var docExist = await _doctorService.DoctorById((int)Item1.Id);
                    if (docExist != null)
                    {
                        if (Item1.ProfilePic != null && Item1.ProfilePic.Contains("base64"))
                        {
                            string fileName = Path.GetRandomFileName() + ".png";
                            byte[] bytes = Convert.FromBase64String(Item1.ProfilePic!.Split("base64,")[1]);
                            MemoryStream memoryStream = new(bytes);
                            IFormFile formFile = new FormFile(memoryStream, 0, bytes.Length, fileName, fileName);
                            if (formFile != null)
                            {
                                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "doctor");
                                (uploadsFolder != null ? new DirectoryInfo(uploadsFolder) : null)?.Create();
                                string filePath = Path.Combine(uploadsFolder!, fileName);
                                using var fileStream = new FileStream(filePath, FileMode.Create);
                                formFile.CopyTo(fileStream);
                            }
                            docExist.ProfilePic = fileName;
                        }
                        if (Item1.Signature != null && Item1.Signature.Contains("base64"))
                        {
                            string fileName = Path.GetRandomFileName() + ".png";
                            byte[] bytes = Convert.FromBase64String(Item1.Signature!.Split("base64,")[1]);
                            MemoryStream memoryStream = new(bytes);
                            IFormFile formFile = new FormFile(memoryStream, 0, bytes.Length, fileName, fileName);
                            if (formFile != null)
                            {
                                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "doctor");
                                (uploadsFolder != null ? new DirectoryInfo(uploadsFolder) : null)?.Create();
                                string filePath = Path.Combine(uploadsFolder!, fileName);
                                using var fileStream = new FileStream(filePath, FileMode.Create);
                                formFile.CopyTo(fileStream);
                            }
                            docExist.Signature = fileName;
                        }
                        docExist.Designation = Item1.Designation;
                        docExist.DepartmentId = Item1.DepartmentId;
                        docExist.Address = Item1.Address;
                        docExist.Specialist = Item1.Specialist;
                        
                        docExist.DOB = Item1.DOB;
                        docExist.Biography = Item1.Biography;
                        docExist.Gender = Item1.Gender;
                        docExist.BloodGroup = Item1.BloodGroup;
                        docExist.Status = Item1.Status;
                        var res= await _doctorService.UpdateDoctor(docExist);
                        var user = await _userManager.FindByEmailAsync(Item1.ApplicationUser!.Email);
                        user.PhoneNumber = Item1.ApplicationUser.PhoneNumber;
                        user.FirstName = Item1.ApplicationUser.FirstName;
                        user.LastName = Item1.ApplicationUser.LastName;
                        await _userManager.UpdateAsync(user);
                        return Json(new { message = CommonResource.UpdateSuccess, status = true });
                    }
                }
                return Json(new { message = CommonResource.UpdateFailed, status = false });
            }
            return Json(new { message = CommonResource.UpdateFailed, status = false });
        }
        [Authorize(Roles = CommonHelper.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor(int Id)
        {
            var check = await _patientService.GetPatientCount(Id, "Doctor");
            if (!check)
            {
                var doctor = await _doctorService.GetDoctorById(Id);
                if (doctor != null)
                {
                    var userExist = await _userManager.FindByEmailAsync(doctor.ApplicationUser!.Email);
                    if (userExist != null)
                    {
                        await _userManager.DeleteAsync(userExist);
                        await _doctorService.DeleteDoctor(Id);

                        return Json(new { message = CommonResource.DeleteSuccess, status = true });
                    }
                    return Json(new { message = CommonResource.DeleteFailed, status = false });
                }
                return Json(new { message = CommonResource.DeleteFailed, status = false });
            }
            else
            {
                return Json(new { message = "Can not delete, patient detail updated by this doctor", status = false });
            }
        }


        [Authorize(Roles = CommonHelper.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorViewModel doctorViewModel)
        {
            switch (ModelState.IsValid)
            {
                case true:
                    var userExist = await _userManager.FindByEmailAsync(doctorViewModel.ApplicationUser!.Email);
                    switch (userExist == null)
                    {
                        case true:
                            if (doctorViewModel.ProfilePic != null)
                            {
                                string fileName = Path.GetRandomFileName() + ".png";
                                byte[] bytes = Convert.FromBase64String(doctorViewModel.ProfilePic!.Split("base64,")[1]);
                                MemoryStream memoryStream = new(bytes);
                                IFormFile formFile = new FormFile(memoryStream, 0, bytes.Length, fileName, fileName);
                                if (formFile != null)
                                {
                                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "doctor");
                                    (uploadsFolder != null ? new DirectoryInfo(uploadsFolder) : null)?.Create();
                                    string filePath = Path.Combine(uploadsFolder!, fileName);
                                    using var fileStream = new FileStream(filePath, FileMode.Create);
                                    formFile.CopyTo(fileStream);
                                }
                                doctorViewModel.ProfilePic = fileName;
                            }
                            if (doctorViewModel.Signature != null)
                            {
                                string fileName = Path.GetRandomFileName() + ".png";
                                byte[] bytes = Convert.FromBase64String(doctorViewModel.Signature!.Split("base64,")[1]);
                                MemoryStream memoryStream = new(bytes);
                                IFormFile formFile = new FormFile(memoryStream, 0, bytes.Length, fileName, fileName);
                                if (formFile != null)
                                {
                                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "doctor");
                                    (uploadsFolder != null ? new DirectoryInfo(uploadsFolder) : null)?.Create();
                                    string filePath = Path.Combine(uploadsFolder!, fileName);
                                    using var fileStream = new FileStream(filePath, FileMode.Create);
                                    formFile.CopyTo(fileStream);
                                }
                                doctorViewModel.Signature = fileName;
                            }
                            await _doctorService.InsertDoctor(doctorViewModel);
                            var user = await _userManager.FindByEmailAsync(doctorViewModel.ApplicationUser!.Email);
                            var result = await _userManager.AddToRoleAsync(user, CommonHelper.Doctor);
                            return result.Succeeded switch { true => Json(new { message = CommonResource.CreateSuccess, status = true }), _ => Json(new { message = CommonResource.UserNotCreated, status = false }) };
                        default:
                            return Json(new { message = CommonResource.AlreadyExist, status = false });
                    }
                default: return Json(new { message = CommonResource.BlankField, status = false });
            }
        }
        public async Task<IActionResult> Dashboard()
        {
            return View(await _dashboardService.GetDoctorDashboardDataAsync(Convert.ToInt32(HttpContext.User.FindFirstValue(CommonHelper.Id))));
        }
    }
}