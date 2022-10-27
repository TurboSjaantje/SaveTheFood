using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Pakket
    {
        [Key]
        public int Id { get; set; }
        public string Naam { get; set; }
        public Kantine kantine { get; set; }
        public string DatumTijd { get; set; }
        public string OphaalTijd { get; set; }
        public bool AchtienPlus { get; set; }
        public double Prijs { get; set; }
        public TypeMaaltijden TypeMaaltijd { get; set; } 
        public Student? GereserveerdDoor { get; set; }

        public ICollection<Product> Producten { get; set; }
    }

    public enum TypeMaaltijden
    {
        [Display(Name = "Warme maaltijd")]
        WarmeMaaltijd,
        [Display(Name = "Koude maaltijd")]
        KoudeMaaltijd,
        [Display(Name = "Middag borrel")]
        MiddagBorrel,
        [Display(Name = "Avond borrel")]
        AvondBorrel,
        [Display(Name = "Brood maaltijd")]
        BroodMaaltijd,
        [Display(Name = "Vegan maaltijd")]
        VeganMaaltijd
    }
}