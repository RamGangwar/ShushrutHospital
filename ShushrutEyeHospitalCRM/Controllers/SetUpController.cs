using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Resources;
using ShushrutEyeHospitalCRM.Services.Implementation;
using ShushrutEyeHospitalCRM.Services.Interface;
using System.Data;

namespace ShushrutEyeHospitalCRM.Controllers
{
    public class SetUpController : Controller
    {
        private readonly IRepositories<CommonEyeProblem> _problems;
        private readonly IRepositories<Medicines> _medicine;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public SetUpController(IRepositories<CommonEyeProblem> problems, UserManager<ApplicationUser> userManager, IMapper mapper, IRepositories<Medicines> medicine)
        {
            _problems = problems;
            _userManager = userManager;
            _mapper = mapper;
            _medicine = medicine;
        }
        [Authorize(Roles = CommonHelper.Admin)]
        
        public async Task<IActionResult> AddEditEyeProblems(int Id = 0)
        {
            var request = new CommonEyeProblemViewModel();
            if (Id > 0)
            {
                var response = await _problems.GetByIdAsync(Id);
                request = (_mapper.Map<CommonEyeProblemViewModel>(response));
            }
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditEyeProblems([FromForm] CommonEyeProblemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProblemId == 0)
                {
                    var modeldata = _mapper.Map<CommonEyeProblem>(model);
                    modeldata.IsActive = true;
                    var save = await _problems.CreateAsync(modeldata);
                }
                else
                {
                    var res = await _problems.GetByIdAsync(model.ProblemId);
                    if (res != null)
                    {
                        res.ProblemName = model.ProblemName;
                        var save =await _problems.UpdateAsync(res);
                    }
                }
                return Json(new { message = "Successfully Done", status = true });
            }
            else
            {
                return Json(new { message = "Failed to save", status = false });
            }
        }

        [Authorize(Roles = CommonHelper.Admin)]
        public async Task<IActionResult> ProblemList()
        {
            var problemList = await _problems.GetAllAsync();
            var data = _mapper.Map<IEnumerable<CommonEyeProblemViewModel>>(problemList);
            return View(data);
        }
        [Authorize(Roles = CommonHelper.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteProblem(int Id)
        {
            var check = await _problems.GetByIdAsync(Id);
            if (check != null)
            {
                await _problems.DeleteAsync(check);
                return Json(new { message = CommonResource.DeleteSuccess, status = true });

            }
            else
            {
                return Json(new { message = "Invalid Problem", status = false });
            }
        }

        [Authorize(Roles = CommonHelper.Admin)]

        public async Task<IActionResult> AddEditMedicine(int Id = 0)
        {
            var request = new MedicineViewModel();
            if (Id > 0)
            {
                var response = await _medicine.GetByIdAsync(Id);
                request = (_mapper.Map<MedicineViewModel>(response));
            }
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditMedicine([FromForm] MedicineViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MedicineId == 0)
                {
                    var modeldata = _mapper.Map<Medicines>(model);
                    modeldata.IsActive = true;
                    var save = await _medicine.CreateAsync(modeldata);
                }
                else
                {
                    var res = await _medicine.GetByIdAsync(model.MedicineId);
                    if (res != null)
                    {
                        res.MedicineName = model.MedicineName;
                        var save = await _medicine.UpdateAsync(res);
                    }
                }
                return Json(new { message = "Successfully Done", status = true });
            }
            else
            {
                return Json(new { message = "Failed to save", status = false });
            }
        }

        [Authorize(Roles = CommonHelper.Admin)]
        public async Task<IActionResult> MedicineList()
        {
            var medicineList = await _medicine.GetAllAsync();
            var data = _mapper.Map<IEnumerable<MedicineViewModel>>(medicineList);
            return View(data);
        }
        [Authorize(Roles = CommonHelper.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteMedicine(int Id)
        {
            var check = await _medicine.GetByIdAsync(Id);
            if (check != null)
            {
                await _medicine.DeleteAsync(check);
                return Json(new { message = CommonResource.DeleteSuccess, status = true });

            }
            else
            {
                return Json(new { message = "Invalid medicine", status = false });
            }
        }

        public async Task<IActionResult> SearchProblem(string prefix)
        {
            var problemList = await _problems.GetAllAsync();
            var res = problemList.Where(w => w.ProblemName.StartsWith(prefix));
            var data = _mapper.Map<IEnumerable<CommonEyeProblemViewModel>>(res);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> SearchMedicine(string prefix)
        {
            var medilist = await _medicine.GetAllAsync();
            if (!string.IsNullOrEmpty(prefix))
            {
                medilist = medilist.Where(w => w.MedicineName.StartsWith(prefix));
            }
            var data = _mapper.Map<IEnumerable<MedicineViewModel>>(medilist);
            return Json(data);
        }
    }
}
