using System;
using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.UI.MVC.Models.dto
{
    public class LanguageDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public LanguageType Type { get; set; }
        public String ReleaseDate { get; set; }
        public double Version { get; set; }
        public ICollection<IdeWithoutLangDto> Ides { get; set; }

        public LanguageDto(Language lang)
        {
            Id = lang.Id;
            Name = lang.Name;
            Type = lang.Type;
            ReleaseDate = lang.ReleaseDate.ToString("dd/MM/yyyy");
            Version = lang.Version;
            Ides = new List<IdeWithoutLangDto>();
            foreach (var i in lang.Ides)
            {
                Ides.Add(new IdeWithoutLangDto(i.Ide));
            }
        }
    }
}