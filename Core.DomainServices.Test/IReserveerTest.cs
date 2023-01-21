global using Xunit;
global using Moq;
using Core.DomainServices;
using Core.DomainServices.Services;
using Domain;

public class IReserveerServiceTest
{
    [Fact]
    public void StudentJongerDanAchttienKanGeenPakketReserverenDatAlcoholBevat()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-17).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };

        var mockPakketRepo = new Mock<IPakketRepository>();
        var reserveerService = new ReserveerService(mockPakketRepo.Object);

        Assert.Throws<Exception>(() => reserveerService.ouderDanAchtien(student, pakket));
    }
    
    [Fact]
    public void StudentJongerDanAchttienKanPakketReserverenDatGeenAlcoholBevat()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-17).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };

        var mockPakketRepo = new Mock<IPakketRepository>();
        var reserveerService = new ReserveerService(mockPakketRepo.Object);

        Assert.True(reserveerService.ouderDanAchtien(student, pakket));
    }
    
    [Fact]
    public void StudentOuderDanAchttienKanPakketReserverenDatAlcoholBevat()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };

        var mockPakketRepo = new Mock<IPakketRepository>();
        var reserveerService = new ReserveerService(mockPakketRepo.Object);

        Assert.True(reserveerService.ouderDanAchtien(student, pakket));
    }
    
    [Fact]
    public void StudentOuderDanAchttienKanPakketReserverenDatGeenAlcoholBevat()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };

        var mockPakketRepo = new Mock<IPakketRepository>();
        var reserveerService = new ReserveerService(mockPakketRepo.Object);

        Assert.True(reserveerService.ouderDanAchtien(student, pakket));
    }

    [Fact]
    public void PakketNogNietGereserveerdDoorAndereStudent()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo. Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket2);
        var reserveerService = new ReserveerService(mockPakketRepo.Object);

        Assert.True(reserveerService.nogNietGereserveerd(student, pakket1));
    }

    [Fact]
    public void PakketAlGereserveerdDoorAndereStudent()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = student
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo. Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket2);
        var reserveerService = new ReserveerService(mockPakketRepo.Object);

        Assert.Throws<Exception>(() => reserveerService.nogNietGereserveerd(student, pakket1));
    }

    [Fact]
    public void StudentHeeftNogGeenReserveringVoorAfhaalDatumPakket()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket3 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.Setup(x => x.Pakketten).Returns(new List<Pakket> { pakket3 });
        var reserveerService = new ReserveerService(mockPakketRepo.Object);

        Assert.True(reserveerService.nogGeenReserveringVoorAfhaaldatum(student, pakket1));
    }
    
    [Fact]
    public void StudentHeeftAlEenReserveringVoorAfhaalDatumPakket()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = student
        };
        pakket1.Id = 1;
        pakket2.Id = 2;
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.Setup(x => x.Pakketten).Returns(new List<Pakket> { pakket2 });
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        Assert.Throws<Exception>(() => reserveerService.nogGeenReserveringVoorAfhaaldatum(student, pakket1));
    }

    [Fact]
    public void StudentProbeertPakketTeReserverenDatBestaat()
    {
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.Setup(x => x.Pakketten).Returns(new List<Pakket> { pakket1 });
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        Assert.True(reserveerService.pakketBestaat(pakket1));
    }
    
    [Fact]
    public void StudentProbeertPakketTeReserverenDatNietBestaat()
    {
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.Setup(x => x.Pakketten).Returns(new List<Pakket> { });
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        Assert.Throws<Exception>(() => reserveerService.pakketBestaat(pakket1));
    }

    [Fact]
    public void StudentReserveerdEenPakketMetGeldigeWaarden()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.SetupSequence(x => x.Pakketten)
            .Returns(new List<Pakket> { pakket1 })
            .Returns(new List<Pakket> { pakket2 });
        mockPakketRepo.Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket1);
        mockPakketRepo.Setup(x => x.ReserveerPakket(pakket1, student)).Returns(pakket1);
        
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        Assert.True(reserveerService.reserveerPakket(student, pakket1));
    }

    [Fact]
    public void StudentReserveerdEenPakketZonderDatHijAchttienIs()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-17).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.SetupSequence(x => x.Pakketten)
            .Returns(new List<Pakket> { pakket1 })
            .Returns(new List<Pakket> { pakket2 });
        mockPakketRepo.Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket1);
        mockPakketRepo.Setup(x => x.ReserveerPakket(pakket1, student)).Returns(pakket1);
        
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        var ex = Assert.Throws<Exception>(() => reserveerService.reserveerPakket(student, pakket1));
        Assert.Equal("Je moet 18 jaar of ouder zijn om dit pakket te reserveren", ex.Message);
    }
    
    [Fact]
    public void StudentReserveerdEenPakketZonderDatHetPakketBestaat()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.SetupSequence(x => x.Pakketten)
            .Returns(new List<Pakket> { })
            .Returns(new List<Pakket> { pakket2 });
        mockPakketRepo.Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket1);
        mockPakketRepo.Setup(x => x.ReserveerPakket(pakket1, student)).Returns(pakket1);
        
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        var res = Assert.Throws<Exception>(() => reserveerService.reserveerPakket(student, pakket1));
        Assert.Equal("Pakket bestaat niet meer", res.Message);
    }

    [Fact]
    public void StudentReserveerdEenPakketTerwijlHijAlEenReserveringVoorDieAfhaaldatumHeeft()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = student
        };

        pakket1.Id = 1;
        pakket2.Id = 2;
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.SetupSequence(x => x.Pakketten)
            .Returns(new List<Pakket> { pakket1 })
            .Returns(new List<Pakket> { pakket2 });
        mockPakketRepo.Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket1);
        mockPakketRepo.Setup(x => x.ReserveerPakket(pakket1, student)).Returns(pakket1);
        
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        var res = Assert.Throws<Exception>(() => reserveerService.reserveerPakket(student, pakket1));
        Assert.Equal("Je hebt al een pakket gereserveerd voor deze datum", res.Message);
    }

    [Fact]
    public void StudentReserveerdEenPakketDatAlGereserveerdIs()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = student
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.SetupSequence(x => x.Pakketten)
            .Returns(new List<Pakket> { pakket1 })
            .Returns(new List<Pakket> { pakket2 });
        mockPakketRepo.Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket1);
        mockPakketRepo.Setup(x => x.ReserveerPakket(pakket1, student)).Returns(pakket1);
        
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        var res = Assert.Throws<Exception>(() => reserveerService.reserveerPakket(student, pakket1));
        Assert.Equal("Dit pakket is al gereserveerd door een andere student", res.Message);
    }

    [Fact]
    public void StudenReserveerdEenPakketMetGeldigeWaardenMaarDeReserveringKanNietOpgeslagenWorden()
    {
        Student student = new Student { Email = "user@gmail.com", Naam = "Daan van der Meulen", StudentNummer = 2189862, GeboorteDatum = DateTime.Now.AddYears(-18).AddSeconds(-DateTime.Now.Second).AddMilliseconds(-DateTime.Now.Millisecond).ToString(), StudieStad = "Breda", TelefoonNummer = "0631490687" };
        Pakket pakket1 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = false,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        Pakket pakket2 = new Pakket
        {
            Naam = "a",
            Producten = null,
            kantine = null,
            DatumTijd = DateTime.Now.ToString(),
            OphaalTijd = DateTime.Now.ToString(),
            AchtienPlus = true,
            Prijs = 4.50,
            TypeMaaltijd = TypeMaaltijden.WarmeMaaltijd,
            GereserveerdDoor = null
        };
        
        var mockPakketRepo = new Mock<IPakketRepository>();
        mockPakketRepo.SetupSequence(x => x.Pakketten)
            .Returns(new List<Pakket> { pakket1 })
            .Returns(new List<Pakket> { pakket2 });
        mockPakketRepo.Setup(x => x.ReadPakket(pakket1.Id)).Returns(pakket1);
        Pakket pakket = null;
        mockPakketRepo.Setup(x => x.ReserveerPakket(pakket1, student)).Returns(pakket);
        
        var reserveerService = new ReserveerService(mockPakketRepo.Object);
        
        var res = Assert.Throws<Exception>(() => reserveerService.reserveerPakket(student, pakket1));
        
        Assert.Equal("Pakket kan niet gereserveerd worden", res.Message);
    }
    
}