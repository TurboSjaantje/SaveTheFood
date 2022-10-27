using Microsoft.AspNetCore;
using Core.DomainServices;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.ViewModels;
using System.Collections.ObjectModel;

namespace Portal.Controllers
{
    public class PakketController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPakketRepository _pakketRepository;
        private readonly IMedewerkerRepository _medewerkerRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IKantineRepository _kantineRepository;

        public PakketController(IProductRepository productRepository, IStudentRepository studentRepository, IPakketRepository pakketRepository, IMedewerkerRepository medewerkerRepository, IKantineRepository kantineRepository)
        {
            _productRepository = productRepository;
            _pakketRepository = pakketRepository;
            _medewerkerRepository = medewerkerRepository;
            _studentRepository = studentRepository;
            _kantineRepository = kantineRepository;
        }

        [Authorize(Policy = "OnlyRegularUsersAndUp")]
        public IActionResult Reserveren()
        {
            ViewBag.pakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor == null).ToList();
            ViewBag.vorigePagina = "Reserveren";

            return View("Reserveren");
        }

        [Authorize(Policy = "OnlyRegularUsersAndUp")]
        public IActionResult ReserveerPakket(int pakketid)
        {
            Student student = _studentRepository.ReadStudent(User.Identity.Name.ToString());
            Pakket pakket = _pakketRepository.ReadPakket(pakketid);

            _pakketRepository.ReserveerPakket(pakket, student);

            return RedirectToAction("Reserveringen");
        }

        [Authorize(Policy = "OnlyPowerUsersAndUp")]
        public IActionResult Pakketbeheer()
        {
            Kantine kantine = _medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString()).Locatie;

            ViewBag.vrijgegevenPakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor == null && p.kantine == kantine);
            ViewBag.gereserveerdePakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor != null && p.kantine == kantine);
            ViewBag.vorigePagina = "Pakketbeheer";

            return View("Pakketbeheer");
        }

        [Authorize(Policy = "OnlyRegularUsersAndUp")]
        public IActionResult Reserveringen()
        {
            ViewBag.pakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor != null && p.GereserveerdDoor.Email.Equals(User.Identity.Name.ToString())).ToList();
            ViewBag.vorigePagina = "Reserveringen";
            return View("Reserveringen", ViewBag);
        }

        [Authorize(Policy = "OnlyRegularUsersAndUp")]
        public IActionResult Box(int id, string vorigepagina)
        {
            ViewBag.pakket = _pakketRepository.ReadPakket(id);
            ViewBag.gereserveerdePakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor != null && p.GereserveerdDoor.Email.Equals(User.Identity.Name.ToString()));
            ViewBag.pakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor != null);
            if (User.HasClaim("UserType","regularuser"))
            {
                ViewBag.iseighteen = false;
                DateTime ophaalDatum = DateTime.Parse(_pakketRepository.ReadPakket(id).OphaalTijd);
                DateTime geboorteDatum = DateTime.Parse(_studentRepository.ReadStudent(User.Identity.Name.ToString()).GeboorteDatum);
                TimeSpan timeSpan = new TimeSpan();
                if ((ophaalDatum - geboorteDatum).TotalDays > (18 * 365)) ViewBag.iseighteen = true;
            }
            ViewBag.vorigePagina = vorigepagina;
            return View("Box", ViewBag);
        }

        [Authorize(Policy = "OnlyPowerUsersAndUp")]
        [HttpGet]
        public IActionResult NieuwPakket()
        {
            ViewBag.producten = _productRepository.Producten.ToList();
            ViewBag.kantine = _kantineRepository.ReadKantine(_medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString()).Locatie.Id);
            ViewBag.typemaaltijden = CreateMealTypeSelectList(ViewBag.kantine);

            PakketViewModel pakketViewModel = new()
            {
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond),
                Producten = new Dictionary<int, bool>()
            };

            foreach (Product product in _productRepository.Producten.ToList())
                pakketViewModel.Producten.Add(product.Id, false);

            return View("NieuwPakket", pakketViewModel);
        }

        [Authorize(Policy = "OnlyPowerUsersAndUp")]
        [HttpPost]
        public IActionResult NieuwPakket(PakketViewModel pakketViewModel)
        {
            if (ModelState.IsValid)
            {
                ICollection<Product> producten = _productRepository.Producten.ToList();
                ICollection<Product> geselecteerdeProducten = new Collection<Product>();
                bool AchtienPlus = false;

                foreach (var productid in pakketViewModel.Producten.Keys)
                {
                    if (pakketViewModel.Producten[productid] == true)
                    {
                        geselecteerdeProducten.Add(producten.FirstOrDefault(p => p.Id == productid));
                        if (producten.FirstOrDefault(p => p.Id == productid).AlcoholHoudend == true) AchtienPlus = true;
                    }
                }

                Pakket pakket = new()
                {
                    Naam = pakketViewModel.Naam,
                    kantine = _medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString()).Locatie,
                    DatumTijd = DateTime.Now.ToString(),
                    OphaalTijd = pakketViewModel.OphaalTijd.ToString(),
                    AchtienPlus = AchtienPlus,
                    Prijs = pakketViewModel.Prijs,
                    TypeMaaltijd = pakketViewModel.TypeMaaltijd,
                    GereserveerdDoor = null,
                    Producten = geselecteerdeProducten
                };

                _pakketRepository.CreatePakket(pakket);
                return RedirectToAction("Pakketbeheer");
            }

            ViewBag.producten = _productRepository.Producten.ToList();

            return View(pakketViewModel);
        }

        [Authorize(Policy = "OnlyPowerUsersAndUp")]
        public IActionResult Pakketten()
        {
            int LocatieId = _medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString()).Locatie.Id;

            ViewBag.eigenPakketten =
                _pakketRepository.Pakketten.Where(p => p.kantine.Id == LocatieId && p.GereserveerdDoor == null).ToList().OrderBy(p => p.DatumTijd);
            ViewBag.anderePakketten =
                _pakketRepository.Pakketten.Where(p => p.kantine.Id != LocatieId && p.GereserveerdDoor == null).ToList().OrderBy(p => p.DatumTijd);

            ViewBag.vorigePagina = "Pakketten";

            return View("Pakketten", ViewBag);
        }

        [Authorize(Policy = "OnlyPowerUsersAndUp")]
        public async Task<IActionResult> VerwijderPakket(int pakketid)
        {
            _pakketRepository.DeletePakket(_pakketRepository.ReadPakket(pakketid));

            return RedirectToAction("Pakketbeheer");
        }

        [Authorize(Policy = "OnlyPowerUsersAndUp")]
        [HttpGet]
        public IActionResult UpdatePakket(int pakketid)
        {
            ViewBag.producten = _productRepository.Producten.ToList();
            ViewBag.id = pakketid;
            Pakket pakket = _pakketRepository.ReadPakket(pakketid);

            PakketViewModelUpdate pakketViewModel = new()
            {
                Id = pakketid,
                Naam = pakket.Naam,
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond),
                Prijs = pakket.Prijs,
                TypeMaaltijd = pakket.TypeMaaltijd,
                Producten = new Dictionary<int, bool>()
            };

            foreach (Product product in _productRepository.Producten.ToList())
            {
                if (pakket.Producten.FirstOrDefault(p => p.Id == product.Id) != null)
                {
                    pakketViewModel.Producten.Add(product.Id, true);
                }
                else
                {
                    pakketViewModel.Producten.Add(product.Id, false);
                }
            }

            return View("UpdatePakket", pakketViewModel);
        }


        [HttpPost]
        public IActionResult UpdatePakket(PakketViewModelUpdate pakketViewModel)
        {
            if (ModelState.IsValid)
            {
                ICollection<Product> producten = _productRepository.Producten.ToList();
                ICollection<Product> geselecteerdeProducten = new Collection<Product>();
                bool AchtienPlus = false;

                foreach (var productid in pakketViewModel.Producten.Keys)
                {
                    if (pakketViewModel.Producten[productid] == true)
                    {
                        geselecteerdeProducten.Add(producten.FirstOrDefault(p => p.Id == productid));
                        if (producten.FirstOrDefault(p => p.Id == productid).AlcoholHoudend == true) AchtienPlus = true;
                    }
                }

                Pakket pakket = new()
                {
                    Naam = pakketViewModel.Naam,
                    kantine = _medewerkerRepository.ReadMedewerker(User.Identity.Name.ToString()).Locatie,
                    DatumTijd = DateTime.Now.ToString(),
                    OphaalTijd = pakketViewModel.OphaalTijd.ToString(),
                    AchtienPlus = AchtienPlus,
                    Prijs = pakketViewModel.Prijs,
                    TypeMaaltijd = pakketViewModel.TypeMaaltijd,
                    GereserveerdDoor = null,
                    Producten = geselecteerdeProducten
                };

                _pakketRepository.UpdatePakket(pakket, pakketViewModel.Id);
                return RedirectToAction("Pakketbeheer");
            }

            ViewBag.producten = _productRepository.Producten.ToList();
            ViewBag.id = pakketViewModel.Id;

            return View("UpdatePakket", pakketViewModel);
        }

        private SelectList? CreateMealTypeSelectList(Kantine kantine)
        {
            List<SelectListItem> list = new();
            if (kantine.WarmeMaaltijden) { list.Add(new SelectListItem { Selected = false, Text = "Warme Maaltijd", Value = "WarmeMaaltijd" }); }
            list.Add(new SelectListItem { Selected = false, Text = "Koude Maaltijd", Value = "KoudeMaaltijd" });
            list.Add(new SelectListItem { Selected = false, Text = "Middag Borrel", Value = "MiddagBorrel" });
            list.Add(new SelectListItem { Selected = false, Text = "Avond Borrel", Value = "AvondBorrel" });
            list.Add(new SelectListItem { Selected = false, Text = "Brood Maaltijd", Value = "BroodMaaltijd" });
            list.Add(new SelectListItem { Selected = false, Text = "Vegan Maaltijd", Value = "VeganMaaltijd" });

            return new SelectList(list, "Value", "Text");
        }
    }
}

