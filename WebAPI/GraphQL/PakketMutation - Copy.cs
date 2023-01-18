using Core.DomainServices;
using Domain;

namespace WebAPI.GraphQL
{
    public class ProductMutation
    {
        private readonly IProductRepository _productRepository;

        public ProductMutation(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Create(Product product) => _productRepository.CreateProduct(product);
    }
}
