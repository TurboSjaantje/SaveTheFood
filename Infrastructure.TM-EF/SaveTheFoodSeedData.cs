using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.TM_EF
{
    public class SaveTheFoodSeedData : ISeedData
    {
        private SaveTheFoodDbContext _context;
        private ILogger<SaveTheFoodSeedData> _logger;

        public SaveTheFoodSeedData(SaveTheFoodDbContext context, ILogger<SaveTheFoodSeedData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task EnsurePopulated(bool dropExisting = true)
        {
            if (dropExisting)
            {
                _context.Studenten.RemoveRange(_context.Studenten);
                _context.Kantines.RemoveRange(_context.Kantines);
                _context.Producten.RemoveRange(_context.Producten);
                _context.Medewerkers.RemoveRange(_context.Medewerkers);
                _context.Pakketten.RemoveRange(_context.Pakketten);
            }

            //DECLARING

            //Declaring student
            Student student1 = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-15).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };

            //Declaring kantines
            Kantine kantine1 = new Kantine { Locatie = "Hogeschoollaan 1", Stad = "Breda", WarmeMaaltijden = false };
            Kantine kantine2 = new Kantine { Locatie = "Lovensdijkstraat 61-63", Stad = "Breda", WarmeMaaltijden = false };
            Kantine kantine3 = new Kantine { Locatie = "Beukenlaan 1", Stad = "Breda", WarmeMaaltijden = true };
            Kantine kantine4 = new Kantine { Locatie = "Bijster 7-21", Stad = "Breda", WarmeMaaltijden = false };
            Kantine kantine5 = new Kantine { Locatie = "Beverweg 4", Stad = "Breda", WarmeMaaltijden = true };
            Kantine kantine6 = new Kantine { Locatie = "Claudius Prinsenlaan 128", Stad = "Breda", WarmeMaaltijden = false };
            Kantine kantine7 = new Kantine { Locatie = "Onderwijsboulevard 215", Stad = "'s-Hertogenbosch", WarmeMaaltijden = true };
            Kantine kantine8 = new Kantine { Locatie = "Hervenplein 2", Stad = "'s-Hertogenbosch", WarmeMaaltijden = true };
            Kantine kantine9 = new Kantine { Locatie = "Parallelweg 64", Stad = "'s-Hertogenbosch", WarmeMaaltijden = false };
            Kantine kantine10 = new Kantine { Locatie = "Stationsplein 50", Stad = "'s-Hertogenbosch", WarmeMaaltijden = false };
            Kantine kantine11 = new Kantine { Locatie = "Parallelweg 23", Stad = "'s-Hertogenbosch", WarmeMaaltijden = false };
            Kantine kantine12 = new Kantine { Locatie = "Statenlaan 29-67", Stad = "'s-Hertogenbosch", WarmeMaaltijden = true };

            //Declaring medewerker
            Medewerker medewerker1 = new Medewerker { Email = "admin@gmail.com", Locatie = kantine1, Naam = "Johan Pietersz", PersoneelsNummer = 1578965 };

            //Declaring producten
            Product product1 = new Product { Naam = "Spinazie", AlcoholHoudend = false, Foto = "/images/product_spinazie.jpg" };
            Product product2 = new Product { Naam = "Prei", AlcoholHoudend = false, Foto = "/images/product_prei.jpg" };
            Product product3 = new Product { Naam = "Bier", AlcoholHoudend = true, Foto = "/images/product_bier.jpg" };
            Product product4 = new Product { Naam = "Wijn", AlcoholHoudend = true, Foto = "/images/product_wijn.jpg" };
            Product product5 = new Product { Naam = "Brood", AlcoholHoudend = false, Foto = "/images/product_brood.jpg" };

            //Declaring pakketen
            Pakket pakket1 = new Pakket
            {
                Naam = "Avondpakket",
                Producten = new Collection<Product> { product1, product2 },
                kantine = kantine1,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = false,
                Prijs = 4.50,
                TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
                GereserveerdDoor = student1
            };
            Pakket pakket2 = new Pakket
            {
                Naam = "Tussendoortje",
                Producten = new Collection<Product> { product3, product4, product5 },
                kantine = kantine1,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddDays(1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = true,
                Prijs = 6.71,
                TypeMaaltijd = TypeMaaltijden.MiddagBorrel,
                GereserveerdDoor = null
            };
            Pakket pakket3 = new Pakket
            {
                Naam = "Avondpakket",
                Producten = new Collection<Product> { product1, product2 },
                kantine = kantine1,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = false,
                Prijs = 4.50,
                TypeMaaltijd = TypeMaaltijden.AvondBorrel,
                GereserveerdDoor = null
            };
            Pakket pakket4 = new Pakket
            {
                Naam = "Tussendoortje",
                Producten = new Collection<Product> { product3, product4, product5 },
                kantine = kantine1,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = true,
                Prijs = 6.71,
                TypeMaaltijd = TypeMaaltijden.MiddagBorrel,
                GereserveerdDoor = null
            };
            Pakket pakket5 = new Pakket
            {
                Naam = "Avondpakket",
                Producten = new Collection<Product> { product1, product2 },
                kantine = kantine2,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = false,
                Prijs = 4.50,
                TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
                GereserveerdDoor = null
            };
            Pakket pakket6 = new Pakket
            {
                Naam = "Tussendoortje",
                Producten = new Collection<Product> { product3, product4, product5 },
                kantine = kantine3,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = true,
                Prijs = 6.71,
                TypeMaaltijd = TypeMaaltijden.KoudeMaaltijd,
                GereserveerdDoor = null
            };
            Pakket pakket7 = new Pakket
            {
                Naam = "Avondpakket",
                Producten = new Collection<Product> { product1, product2 },
                kantine = kantine4,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = false,
                Prijs = 4.50,
                TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
                GereserveerdDoor = null
            };
            Pakket pakket8 = new Pakket
            {
                Naam = "Tussendoortje",
                Producten = new Collection<Product> { product3, product4, product5 },
                kantine = kantine5,
                DatumTijd = DateTime.Now.AddDays(-1).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                OphaalTijd = DateTime.Now.AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(),
                AchtienPlus = true,
                Prijs = 6.71,
                TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
                GereserveerdDoor = null
            };

            //SEEDING
            //Studenten seeden
            _logger.LogInformation("Preparing to seed Students");
            _context.Studenten.Add(student1);

            //Kantines seeden
            _logger.LogInformation("Preparing to seed Kantines");
            _context.Kantines.AddRange(new[]{
                kantine1, kantine2, kantine3, kantine4, kantine5, kantine6, kantine7, kantine8, kantine9, kantine10, kantine11, kantine12});
            _context.SaveChanges();

            //Medewerkers
            _logger.LogInformation("Preparing to seed Medewerkers");
            _context.Medewerkers.Add(medewerker1);

            //Producten
            _logger.LogInformation("Preparing to seed Producten");
            _context.Producten.AddRange(new[] { product1, product2, product3, product4, product5 });
            _context.SaveChanges();

            //Pakketten
            _logger.LogInformation("Preparing to seed Pakketten");
            _context.Pakketten.AddRange(new[] { pakket1, pakket2, pakket3, pakket4, pakket5, pakket6, pakket7, pakket8 });
            _context.SaveChanges();

            _logger.LogInformation("Everything seeded!");
        }
    }
}
