using Core.DomainServices;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TM_EF
{
    public class MedewerkerRepository : IMedewerkerRepository
    {
        private readonly SaveTheFoodDbContext _dbcontext;

        public MedewerkerRepository(SaveTheFoodDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Medewerker> Medewerkers => _dbcontext.Medewerkers.Include(k => k.Locatie).ToList();

        public Medewerker? CreateMedewerker(Medewerker medewerker)
        {
            _dbcontext.Medewerkers.Add(medewerker);
            _dbcontext.SaveChanges();

            return medewerker;
        }

        public Medewerker? ReadMedewerker(string email)
        { 
            return _dbcontext.Medewerkers.Include(k => k.Locatie).FirstOrDefault(m => m.Email == email);
        }

        public Medewerker? UpdateMedewerker(Medewerker medewerker)
        {
            var entityToUpdate = _dbcontext.Medewerkers.FirstOrDefault(m => m.Email == medewerker.Email);
            if (entityToUpdate != null)
            {
                entityToUpdate.Naam = medewerker.Naam;
                entityToUpdate.PersoneelsNummer = medewerker.PersoneelsNummer;
                entityToUpdate.Locatie = medewerker.Locatie;

                _dbcontext.SaveChanges();
            }

            return entityToUpdate;
        }

        public Medewerker? DeleteMedewerker(Medewerker medewerker)
        {
            var entityToRemove = _dbcontext.Medewerkers.FirstOrDefault(m => m.Email == medewerker.Email);
            if (entityToRemove != null)
            {
                _dbcontext.Medewerkers.Remove(entityToRemove);
                _dbcontext.SaveChanges();
            }

            return entityToRemove;
        }
    }
}
