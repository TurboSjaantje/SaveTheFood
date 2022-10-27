using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Student
    {
        [Key]
        public string Email { get; set; }
        public string Naam { get; set; }
        public string GeboorteDatum { get; set; }
        public int StudentNummer { get; set; }
        public string StudieStad { get; set; }
        public string TelefoonNummer { get; set; }
    }
}