using Core.DomainServices;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TM_EF
{
    public class PakketRepository : IPakketRepository
    {
        SaveTheFoodDbContext _dbcontext;

        public PakketRepository(SaveTheFoodDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Pakket> Pakketten => _dbcontext.Pakketten.Include(s => s.GereserveerdDoor).Include(k => k.kantine).Include(p => p.Producten).ToList();

        public Pakket? CreatePakket(Pakket pakket)
        {
            _dbcontext.Pakketten.Add(pakket);
            _dbcontext.SaveChanges();

            return pakket;
        }

        public Pakket? ReadPakket(int Id)
        {
            return _dbcontext.Pakketten.Include(s => s.GereserveerdDoor).Include(k => k.kantine).Include(p => p.Producten).Include(k => k.kantine).FirstOrDefault(p => p.Id == Id);
        }

        public Pakket? UpdatePakket(Pakket pakket, int pakketid)
        {
            var entityToUpdate = _dbcontext.Pakketten.Include(p => p.Producten).FirstOrDefault(p => p.Id == pakketid);
            if (entityToUpdate != null)
            {
                entityToUpdate.Naam = pakket.Naam;
                entityToUpdate.Producten = pakket.Producten;
                entityToUpdate.kantine = pakket.kantine;
                entityToUpdate.DatumTijd = pakket.DatumTijd;
                entityToUpdate.OphaalTijd = pakket.OphaalTijd;
                entityToUpdate.AchtienPlus = pakket.AchtienPlus;
                entityToUpdate.Prijs = pakket.Prijs;
                entityToUpdate.TypeMaaltijd = pakket.TypeMaaltijd;
                entityToUpdate.GereserveerdDoor = pakket.GereserveerdDoor;

                _dbcontext.SaveChanges();
            }

            return entityToUpdate;
        }

        public Pakket? DeletePakket(Pakket pakket)
        {
            var entityToRemove = _dbcontext.Pakketten.FirstOrDefault(p => p.Id == pakket.Id);
            if (entityToRemove != null)
            {
                _dbcontext.Pakketten.Remove(entityToRemove);
                _dbcontext.SaveChanges();
            }

            return entityToRemove;
        }

        public Pakket? ReserveerPakket(Pakket pakket, Student student)
        {
            var entityToUpdate = _dbcontext.Pakketten.FirstOrDefault(p => p.Id == pakket.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate.GereserveerdDoor = student;

                _dbcontext.SaveChanges();
            }

            return entityToUpdate;
        }
    }
}
