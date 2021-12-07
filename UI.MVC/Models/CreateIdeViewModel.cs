using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Languages.UI.MVC.Models
{
    public class CreateIdeViewModel : IValidatableObject
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name can not be under 3 characters long.")]
        [MaxLength(15, ErrorMessage = "Name can not be over 15 characters long.")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Manufacturer can not be under 3 characters long.")]
        public string Manufacturer { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        [Range(0, 10, ErrorMessage = "Supported Languages should be within range 0-10.")]
        public int? SupportedLanguages { get; set; }
        [Range(0, 3000, ErrorMessage = "Price should be within range 0-3000")]
        public double? Price { get; set; }
        
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