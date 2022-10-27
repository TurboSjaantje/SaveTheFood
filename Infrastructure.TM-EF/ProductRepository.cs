using Core.DomainServices;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TM_EF
{
    public class ProductRepository : IProductRepository
    {
        private readonly SaveTheFoodDbContext _dbcontext;

        public ProductRepository(SaveTheFoodDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Product> Producten => _dbcontext.Producten.ToList();

        public Product? CreateProduct(Product product)
        {
            _dbcontext.Producten.Add(product);
            _dbcontext.SaveChanges();

            return product;
        }

        public Product? ReadProduct(int id)
        {
            return _dbcontext.Producten.FirstOrDefault(p => p.Id == id);
        }
        public Product? UpdateProduct(Product product)
        {
            var entityToUpdate = _dbcontext.Producten.FirstOrDefault(p => p.Id == product.Id);   
            if (entityToUpdate != null)
            {
                entityToUpdate.AlcoholHoudend = product.AlcoholHoudend;
                entityToUpdate.Naam = product.Naam;
                entityToUpdate.Foto = product.Foto;

                _dbcontext.SaveChanges();
            }

            return entityToUpdate;
        }

        public Product? DeleteProduct(Product product)
        {
            var entityToRemove = _dbcontext.Producten.FirstOrDefault(p => p.Id == product.Id);
            if (entityToRemove != null)
            {
                _dbcontext.Producten.Remove(entityToRemove);
                _dbcontext.SaveChanges();
            }

            return entityToRemove;
        }
    }
}
