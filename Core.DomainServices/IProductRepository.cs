using Domain;

namespace Core.DomainServices
{
    public interface IProductRepository
    {
        IEnumerable<Product> Producten { get; }

        Product? CreateProduct(Product Product);
        Product? ReadProduct(int id);
        Product? UpdateProduct(Product Product);
        Product? DeleteProduct(Product Product);
    }
}