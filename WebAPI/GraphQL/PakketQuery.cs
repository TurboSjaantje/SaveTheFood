using Core.DomainServices;
using Domain;

namespace WebAPI.GraphQL
{
    public class PakketQuery
    {
        private readonly IPakketRepository _pakketRepository;

        public PakketQuery(IPakketRepository pakketRepository)
        {
            _pakketRepository = pakketRepository;
        }
        public IQueryable<Pakket> pakket => _pakketRepository.Pakketten.AsQueryable();
    }
}
