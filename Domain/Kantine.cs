using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Kantine
    {
        [Key]
        public int Id { get; set; }
        public string Stad { get; set; } 
        public string Locatie { get; set; } 
        public bool WarmeMaaltijden { get; set; }
    }

}