using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Portal.ViewModels;
public class PakketViewModel
{
    [Required]
    public string Naam { get; set; } = null!;
    [Required]
    [ValidateDateRange]
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

public class ValidateDateRange : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime datetime = Convert.ToDateTime(value);
            if (!(datetime >= DateTime.Now && datetime <= DateTime.Now.AddDays(2)))
            {
                return new ValidationResult("Datum mag maximaal 2 dagen in de toekomst zijn!");
            }
        }
        return ValidationResult.Success;
    }
}

public class AtLeastOneSelectedProduct : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            bool atLeastOneSelected = false;
            var dic = value as Dictionary<int, bool>;
            foreach (bool item in dic.Values) if (item == true) atLeastOneSelected = true;
            
            if (!atLeastOneSelected)
            {
                return new ValidationResult("Er moet minimaal 1 product geselecteerd zijn!");
            }
        }
        return ValidationResult.Success;
    }
}

