using Core.DomainServices;
using Domain;

namespace WebAPI.GraphQL
{
    public class ProductQuery
    {
        private readonly IProductRepository _productRepository;

        public ProductQuery(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IQueryable<Product> product => _productRepository.Producten.AsQueryable();
    }
}
