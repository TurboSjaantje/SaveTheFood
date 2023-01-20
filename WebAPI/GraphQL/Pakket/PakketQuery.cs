using Core.DomainServices;
using Domain;

namespace WebAPI.GraphQL
{
    public class PakketQuery
    {
        private readonly IPakketRepository _pakketRepository;
        private readonly IProductRepository _productRepository;

        public PakketQuery(IPakketRepository pakketRepository, IProductRepository productRepository)
        {
            _pakketRepository = pakketRepository;
            _productRepository = productRepository;
        }
        public IQueryable<Pakket> pakketten => _pakketRepository.Pakketten.ToList().AsQueryable();
        public Pakket pakket(int id) => _pakketRepository.ReadPakket(id);
        public IQueryable<Product> producten => _productRepository.Producten.ToList().AsQueryable();
    }
}
