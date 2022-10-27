﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Portal.ViewModels;
using Core.DomainServices;
using Domain;

namespace Portal.Controllers;
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IStudentRepository _studentRepository;
    private readonly IMedewerkerRepository _medewerkerRepository;
    private readonly IKantineRepository _kantineRepository;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IStudentRepository studentRepository, IKantineRepository kantineRepository, IMedewerkerRepository medewerkerRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _studentRepository = studentRepository;
        _medewerkerRepository = medewerkerRepository;
        _kantineRepository = kantineRepository;
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
            ViewBag.student = _studentRepository.ReadStudent(User.Identity.Name.ToString());
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