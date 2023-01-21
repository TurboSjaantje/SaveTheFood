﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Pakket tempPakket = _pakketRepository.ReadPakket(pakket.Id) ?? null;
            if (tempPakket.GereserveerdDoor != null)
                throw new Exception("Dit pakket is al gereserveerd door een andere student");
            else return true;
        }

        public bool? nogGeenReserveringVoorAfhaaldatum(Student student, Pakket pakket)
        {
            List<Pakket> pakketten = _pakketRepository.Pakketten.Where(p => p.GereserveerdDoor == student).ToList();
            foreach (Pakket loopPakket in pakketten)
            {
                if (DateTime.Parse(loopPakket.OphaalTijd).DayOfYear == DateTime.Parse(pakket.OphaalTijd).DayOfYear && 
                loopPakket.Id != 
                pakket.Id)
                    throw new Exception("Je hebt al een pakket gereserveerd voor deze datum");
            }

            return true;
        }

        public bool? pakketBestaat(Pakket pakket)
        {
            if (_pakketRepository.Pakketten.Where(p => p.Id == pakket.Id).FirstOrDefault() == null)
                throw new Exception("Pakket bestaat niet meer");
            else return true;
        }

        public bool? SaveChanges(Pakket pakket, Student student)
        {
            try
            {
                Pakket pakket2 = this._pakketRepository.ReserveerPakket(pakket, student);
                if (pakket2 != null) return true;
                else throw new Exception("Pakket kan niet gereserveerd worden");
            }
            catch (Exception e)
            {
                throw new Exception("Pakket kan niet gereserveerd worden");
            }
        }

        public bool? reserveerPakket(Student student, Pakket pakket)
        {
            try
            {

                try { this.pakketBestaat(pakket); }
                catch (Exception e) { throw new Exception(e.Message); }
                
                try { this.ouderDanAchtien(student, pakket); }
                catch (Exception e) { throw new Exception(e.Message); }

                try { this.nogGeenReserveringVoorAfhaaldatum(student, pakket); }
                catch (Exception e) { throw new Exception(e.Message); }

                try { this.nogNietGereserveerd(student, pakket); }
                catch (Exception e) { throw new Exception(e.Message); }
                
                try { this.SaveChanges(pakket, student); }
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