using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Languages.BL.Domain
{
    public class Ide : IValidatableObject
    {
        [Key] public long Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Name can not be under 3 characters long.")]
        [MaxLength(15, ErrorMessage = "Name can not be over 15 characters long.")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Manufacturer can not be under 3 characters long.")]
        public string Manufacturer { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public ICollection<IdeLanguage> Languages { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Supported Languages should be within range 0-10.")]
        public int SupportedLanguages { get; set; }

        [Range(0, 3000, ErrorMessage = "Price should be within range 0-3000")]
        public double? Price { get; set; }

        public Ide(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price)
        {
            Name = name;
            Manufacturer = manufacturer;
            ReleaseDate = releaseDate;
            SupportedLanguages = supportedLanguages;
            Price = price;
        }
        
        
        /*
        public Ide(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price, ICollection<IdeLanguage> lang)
        {
            Name = name;
            Manufacturer = manufacturer;
            ReleaseDate = releaseDate;
            SupportedLanguages = supportedLanguages;
            Price = price;
            Languages = lang;
        }
        */

        public override string ToString()
        {
            return
                $"{Name,-15} created by {Manufacturer,10} (released {ReleaseDate:dd/MM/yyyy}) for {SupportedLanguages,2} language(s) price: {(Price != null ? Price : 0.00)}";
            // return Name + " created by" + Manufacturer + " (released " + FormattedRelease() + ") for "+ SupportedLanguages  +" languages price: " + Price;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (ReleaseDate > DateTime.Now)
            {
                string errorMessage = "IDE can't be added before release, come back later.";
                errors.Add(new ValidationResult(errorMessage,
                    new[] {"ReleaseDate"}));
            }

            if (ReleaseDate == new DateTime(1, 1, 1))
            {
                string errorMessage = "Release date is invalid/in the wrong format";
                errors.Add(new ValidationResult(errorMessage,
                    new[] {"ReleaseDate"}));
            }

            return errors;
        }
    }
}