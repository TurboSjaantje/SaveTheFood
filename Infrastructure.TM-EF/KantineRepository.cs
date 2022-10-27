using Core.DomainServices;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TM_EF
{
    public class KantineRepository : IKantineRepository
    {
        private readonly SaveTheFoodDbContext _dbcontext;

        public KantineRepository(SaveTheFoodDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Kantine> Kantines => _dbcontext.Kantines.ToList();

        public Kantine? CreateKantine(Kantine kantine)
        {
            _dbcontext.Kantines.Add(kantine);
            _dbcontext.SaveChanges();

            return kantine;
        }

        public Kantine? ReadKantine(int id)
        {
            return _dbcontext.Kantines.FirstOrDefault(k => k.Id == id);
        }
        public Kantine? UpdateKantine(Kantine kantine)
        {
            var entityToUpdate = _dbcontext.Kantines.FirstOrDefault(k => k.Id == kantine.Id);   
            if (entityToUpdate != null)
            {
                entityToUpdate.Stad = kantine.Stad;
                entityToUpdate.WarmeMaaltijden = kantine.WarmeMaaltijden;
                entityToUpdate.Locatie = kantine.Locatie;

                _dbcontext.SaveChanges();
            }

            return entityToUpdate;
        }

        public Kantine? DeleteKantine(Kantine kantine)
        {
            var entityToRemove = _dbcontext.Kantines.FirstOrDefault(k => k.Id == kantine.Id);
            if (entityToRemove != null)
            {
                _dbcontext.Kantines.Remove(entityToRemove);
                _dbcontext.SaveChanges();
            }

            return entityToRemove;
        }
    }
}
