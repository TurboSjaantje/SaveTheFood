using Domain;

namespace Core.DomainServices
{
    public interface IMedewerkerRepository
    {
        IEnumerable<Medewerker> Medewerkers { get; }

        Medewerker? CreateMedewerker(Medewerker medewerker);
        Medewerker? ReadMedewerker(string email);
        Medewerker? UpdateMedewerker(Medewerker medewerker);
        Medewerker? DeleteMedewerker(Medewerker medewerker);
    }
}