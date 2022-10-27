using Domain;

namespace Core.DomainServices
{
    public interface IPakketRepository
    {
        IEnumerable<Pakket> Pakketten { get; }

        Pakket? CreatePakket(Pakket Pakket);
        Pakket? ReadPakket(int Id);
        Pakket? UpdatePakket(Pakket Pakket, int pakketid);
        Pakket? DeletePakket(Pakket Pakket);
        Pakket? ReserveerPakket(Pakket Pakket, Student Student);
    }
}