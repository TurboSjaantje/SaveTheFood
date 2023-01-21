using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Portal.ViewModels;
using Core.DomainServices;
using Domain;
using Domain.Extensions;

namespace Portal.Controllers;
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IStudentRepository _studentRepository;
    private readonly IMedewerkerRepository _medewerkerRepository;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IStudentRepository studentRepository, IMedewerkerRepository medewerkerRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _studentRepository = studentRepository;
        _medewerkerRepository = medewerkerRepository;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View(new LoginViewModel { ReturnUrl = "/Home/Index" });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var user = await _userManager.FindByNameAsync(loginVM.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Incorrect emailaddress or passowrd or account is locekd");
            return View();
        }

        var signinResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);

        if (!signinResult.Succeeded)
        {
            ModelState.AddModelError("", "Incorrect emailaddress or passowrd or account is locekd");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize(Policy = "OnlyRegularUsersAndUp")]
    public IActionResult Profiel()
    {
        if (User.HasClaim("UserType", "poweruser"))
        {
            ViewBag.medewerker = _medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString());
        }
        else if (User.HasClaim("UserType", "regularuser"))
        {
            Student student = _studentRepository.ReadStudent(User.Identity.Name.ToString());
            ViewBag.student = student;
            ViewBag.age = student.GetAge();
        }

        return View("Profiel", ViewBag);
    }

    [Authorize(Policy = "OnlyRegularUsersAndUp")]
    public async Task<RedirectToActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Home");
    }
}
