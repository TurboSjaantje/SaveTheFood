using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Naam { get; set; }
        public bool AlcoholHoudend { get; set; }
        public string? Foto { get; set; }

        public ICollection<Pakket> pakketten { get; set; }
    }
}