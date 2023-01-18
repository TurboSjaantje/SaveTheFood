global using Xunit;
global using Moq;
using Core.DomainServices;
using Core.DomainServices.Services;
using Domain;
using Infrastructure.TM_EF;

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
}