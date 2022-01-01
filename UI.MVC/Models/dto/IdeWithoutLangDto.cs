using System;
using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.UI.MVC.Models.dto
{
    public class IdeWithoutLangDto
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public String ReleaseDate { get; set; }
        public double? Price { get; set; }
        public IdeWithoutLangDto(Ide ide)
        {
            Name = ide.Name;
            Manufacturer = ide.Manufacturer;
            ReleaseDate = ide.ReleaseDate.ToString("dd/MM/yyyy");
            Price = ide.Price;
        }
    }
}