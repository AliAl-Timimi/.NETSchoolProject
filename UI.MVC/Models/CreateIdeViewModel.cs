using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Languages.BL.Domain;

namespace Languages.UI.MVC.Models
{
    public class CreateIdeViewModel
    {
        public CreateIdeViewModel()
        {
        }


        public CreateIdeViewModel(string name, string manufacturer, DateTime releaseDate, int? supportedLanguages, double? price, IList<Language> languages)
        {
            Name = name;
            Manufacturer = manufacturer;
            ReleaseDate = releaseDate;
            SupportedLanguages = supportedLanguages;
            Price = price;
            Languages = languages;
        }


        [Required]
        [MinLength(3, ErrorMessage = "Name can not be under 3 characters long.")]
        [MaxLength(15, ErrorMessage = "Name can not be over 15 characters long.")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Manufacturer can not be under 3 characters long.")]
        public string Manufacturer { get; set; }

        [RealeaseDate] 
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Supported Languages should be within range 0-10.")]
        public int? SupportedLanguages { get; set; }

        [Range(0, 3000, ErrorMessage = "Price should be within range 0-3000")]
        public double? Price { get; set; }

        public IList<Language> Languages { get; set; }
    }
    public sealed class RealeaseDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string message;
            var dt = (DateTime) value;
            if (dt > DateTime.Now)
            {
                message = "IDE can't be added before release, come back later.";
                return new ValidationResult(message);
            }
            if (dt == new DateTime(1, 1, 1))
            {
                message = "Release date is invalid/in the wrong format";
                return new ValidationResult(message);
            }
            return ValidationResult.Success;
        }
    }
}