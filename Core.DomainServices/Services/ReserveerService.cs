using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Core.DomainServices.Services
{
    public class ReserveerService : IReserveerService
    {

        private readonly IPakketRepository _pakketRepository;

        public ReserveerService(IPakketRepository pakketRepository)
        {
            _pakketRepository = pakketRepository;
        }

        public bool? ouderDanAchtien(Student student, Pakket pakket)
        {
            if (pakket.AchtienPlus)
            {
                DateTime ophaalDatum = DateTime.Parse(pakket.OphaalTijd);
                DateTime geboorteDatum = DateTime.Parse(student.GeboorteDatum);
                if ((ophaalDatum - geboorteDatum).TotalDays > (18 * 365)) return true;
                else throw new Exception("Je moet 18 jaar of ouder zijn om dit pakket te reserveren");
            }
            else
            {
                return true;
            }
        }

        public bool? nogNietGereserveerd(Student student, Pakket pakket)
        {
            throw new NotImplementedException();
        }

        public bool? nogGeenReserveringVoorAfhaaldatum(Student student, Pakket pakket)
        {
            if (this._pakketRepository.Pakketten.filter(p => p.GereserveerdDoor == student && DateTime.Parse(p.OphaalTijd) == DateTime.Parse(pakket.OphaalTijd)).Count() > 0)
            {
                throw new Exception("Je hebt al een pakket gereserveerd voor deze datum");
            }
            else
            {
                return true;
            }
        }

        public bool? reserveerPakket(Student student, Pakket pakket)
        {
            try
            {

                try { this.ouderDanAchtien(student, pakket); }
                catch (Exception e) { throw new Exception(e.Message); }

                try { this.nogGeenReserveringVoorAfhaaldatum(student, pakket); }
                catch (Exception e) { throw new Exception(e.Message); }

                try { this.nogNietGereserveerd(student, pakket); }
                catch (Exception e) { throw new Exception(e.Message); }

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}