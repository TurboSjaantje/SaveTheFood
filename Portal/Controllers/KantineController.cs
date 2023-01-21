using Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Portal.ViewModels;

namespace Portal.Controllers
{
    public class KantineController : Controller
    {
        private readonly IKantineRepository _kantineRepository;

        public KantineController(IKantineRepository kantineRepository)
        {
            _kantineRepository = kantineRepository;
        }

        public IActionResult Locaties()
        {
            ViewBag.kantines = _kantineRepository.kantines;
            return View("Locaties", ViewBag);
        }
    }
}
