using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.BL.Domain
{
    public class IDE : IValidatableObject
    {
        public long Id { get; set; }
        [MinLength(3)]
        [MaxLength(35)]
        public string Name { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public DateTime ReleaseDate { get; }
        public ICollection<Language> Languages { get; set; }
        [Range(0, 10)]
        public int SupportedLanguages { get; set; }
        [Range(0,3000)]
        public double? Price { get; set; }

        public IDE(string name, string manufacturer, DateTime releaseDate, int supportedLanguages, double? price)
        {
            Name = name;
            Manufacturer = manufacturer;
            ReleaseDate = releaseDate;
            SupportedLanguages = supportedLanguages;
            Price = price;
        }

        public override string ToString()
        {
            return
                $"{Name,-10} created by {Manufacturer,10} (released {ReleaseDate:dd/MM/yyyy}) for {SupportedLanguages,2} language(s) price: {(Price != null ? Price : 0.00)}";
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