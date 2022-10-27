using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.ViewModels;
public class PakketViewModelUpdate
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Naam { get; set; } = null!;
    [Required]
    [DisplayName("Op te halen op:")]
    public DateTime OphaalTijd { get; set; }
    [Required]
    [Range(0.01, 1000)]
    public double Prijs { get; set; }
    [Required]
    public TypeMaaltijden TypeMaaltijd { get; set; }
    [Required]
    [AtLeastOneSelectedProduct]
    public IDictionary<int, bool> Producten { get; set; }
}
