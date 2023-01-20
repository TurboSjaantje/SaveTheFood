using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Core.DomainServices.Services
{
    public interface IReserveerService
    {

        bool? ouderDanAchtien(Student student, Pakket pakket);

        bool? nogNietGereserveerd(Student student, Pakket pakket);

        bool? nogGeenReserveringVoorAfhaaldatum(Student student, Pakket pakket);

        bool? pakketBestaat(Pakket pakket);

        public bool? SaveChanges(Pakket pakket, Student student);

        bool? reserveerPakket(Student student, Pakket pakket);

    }
}
