using Domain;

namespace Core.DomainServices
{
    public interface IKantineRepository
    {
        IEnumerable<Kantine> Kantines { get; }

        Kantine? CreateKantine(Kantine kantine);
        Kantine? ReadKantine(int id);
        Kantine? UpdateKantine(Kantine kantine);
        Kantine? DeleteKantine(Kantine kantine);
    }
}