using Microsoft.AspNetCore.Mvc;
using Core.DomainServices;
using Infrastructure.TM_EF;
using Microsoft.AspNetCore.Authorization;

namespace Portal.Controllers;

public class HomeController : Controller
{

    private readonly IStudentRepository _studentRepository;
    private readonly IMedewerkerRepository _medewerkerRepository;
    private readonly IPakketRepository _pakketRepository;

    public HomeController(IStudentRepository studentRepository, IMedewerkerRepository medewerkerRepository, IPakketRepository payketRepository)
    {
        _studentRepository = studentRepository;
        _medewerkerRepository = medewerkerRepository;
        _pakketRepository = payketRepository;
    }

    public IActionResult Login()
    {
        return RedirectToAction("Login", "Account");
    }

    [Authorize(Policy = "OnlyRegularUsersAndUp")]
    public IActionResult Index()
    {
        if (User.HasClaim("UserType", "poweruser"))
        {
            ViewBag.medewerker = _medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString());
            ViewBag.pakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor == null && p.kantine == _medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString()).Locatie).ToList();
        }
        else if (User.HasClaim("UserType", "regularuser"))
        {
            ViewBag.student = _studentRepository.ReadStudent(User.Identity.Name.ToString());
            ViewBag.pakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor == null).ToList();
        }

        ViewBag.vorigePagina = "Home";

        return View("Index", ViewBag);
    }

    public IActionResult AboutUs()
    {
        return View("AboutUs");
    }

    public IActionResult Contact()
    {
        return View("Contact");
    }

    public IActionResult FAQ()
    {
        return View("FAQ");
    }

    public IActionResult Locaties()
    {
        return RedirectToAction("Locaties", "Kantine");
    }
}