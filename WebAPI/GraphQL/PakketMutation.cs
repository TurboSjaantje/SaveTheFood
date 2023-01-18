using Core.DomainServices;
using Domain;

namespace WebAPI.GraphQL
{
    public class PakketMutation
    {
        private readonly IPakketRepository _pakketRepository;

        public PakketMutation(IPakketRepository pakketRepository)
        {
            _pakketRepository = pakketRepository;
        }

        public async Task<Pakket> Create(Pakket pakket) => _pakketRepository.CreatePakket(pakket);
        public async Task<Pakket> Delete(Pakket pakket) => _pakketRepository.DeletePakket(pakket);
    }
}
