using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Medewerker
    {
        [Key]
        public string Email { get; set; }
        public string Naam { get; set; }
        public int PersoneelsNummer { get; set; }
        public Kantine Locatie { get; set; }
    }
}