using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShushrutEyeHospitalCRM.Helper;
using ShushrutEyeHospitalCRM.Models;
using ShushrutEyeHospitalCRM.Models.DTO;
using ShushrutEyeHospitalCRM.Resources;
using System.Security.Claims;
using System.Security.Principal;

namespace ShushrutEyeHospitalCRM.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthorizationController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.User.FindFirstValue(ClaimTypes.Email)))
            {
                var user = await _userManager.FindByEmailAsync(HttpContext.User.FindFirstValue(ClaimTypes.Email));
                if (user != null)
                {
                    await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));
                }
            }
            await _signInManager.SignOutAsync();
            var users = await _userManager.Users.ToListAsync();
            if (users.Any())
            {
                return View();
            }
            return RedirectToAction("SignUp");
        }
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
                    if (result.Succeeded)
                    {
                        IEnumerable<string> roles = await _userManager.GetRolesAsync(user);
                        List<Claim> claims = new()
                        {
                            new Claim(CommonHelper.Name, user.FirstName!),
                            new Claim(ClaimTypes.Role, roles.FirstOrDefault()!),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(CommonHelper.Id, roles.FirstOrDefault()! switch
                            {
                                CommonHelper.Doctor => user.Doctors!.Where(x => x.UserId == user.Id).FirstOrDefault()!.Id.ToString(),
                                CommonHelper.Refraction => user.Refractions!.Where(x => x.UserId == user.Id).FirstOrDefault()!.Id.ToString(),
                                CommonHelper.Reception => user.Receptionists!.Where(x => x.UserId == user.Id).FirstOrDefault()!.Id.ToString(),
                                CommonHelper.Counsling => user.Counslings!.Where(x => x.UserId == user.Id).FirstOrDefault()!.Id.ToString(),
                                _ => "",
                            })
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        // Sign in the user with the new ClaimsPrincipal
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity));

                       
                        //await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));
                        //await _userManager.AddClaimsAsync(user, claims);

                        return Json(new { message = "Logged In", status = true, url = CommonHelper.GetDashboardUrl(roles.FirstOrDefault()!) });
                    }
                    else
                    {
                        return Json(new { message = "Log In failed, Please try again later or contact an administrator.", status = false });
                    }
                }
                else
                {
                    return Json(new { message = "Email or password is incorrect", status = false });
                }
            }
            else
            {
                return Json(new { message = "Please enter email or password", status = false });
            }
        }

        public async Task<IActionResult> SignUp()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users.Any())
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(applicationUserViewModel.Email))
            {
                var roles = await _roleManager.Roles.ToListAsync();
                if (!roles.Any())
                {
                    roles.Add(new ApplicationRole() { Name = CommonHelper.Admin });
                    roles.Add(new ApplicationRole() { Name = CommonHelper.Doctor });
                    roles.Add(new ApplicationRole() { Name = CommonHelper.Patient });
                    roles.Add(new ApplicationRole() { Name = CommonHelper.Reception });
                    roles.Add(new ApplicationRole() { Name = CommonHelper.Refraction });
                    roles.Add(new ApplicationRole() { Name = CommonHelper.Counsling });
                    foreach (var item in roles)
                    {
                        await _roleManager.CreateAsync(item);
                    }
                }
                var result = await _userManager.CreateAsync(_mapper.Map<ApplicationUser>(applicationUserViewModel), applicationUserViewModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(applicationUserViewModel.Email), CommonHelper.Admin);
                    return Json(new { message = CommonResource.CreateSuccess, status = true, url = CommonHelper.LoginUrl });
                }
                else
                {
                    return Json(new { message = CommonResource.CreateFailed, status = false });
                }
            }
            else
            {
                return Json(new { message = CommonResource.BlankField, status = false });
            }
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
