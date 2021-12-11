using System.Collections;
using System.Collections.Generic;
using Languages.BL.Domain;

namespace Languages.UI.MVC.Models
{
    public class EditIdeWithLanguageViewModel
    {
        public EditIdeWithLanguageViewModel(EditIdeViewModel ivm, IEnumerable<Language> languages)
        {
            EditIdeViewModel = ivm;
            Languages = languages;
        }

        public EditIdeViewModel EditIdeViewModel { get; set; }
        public IEnumerable<Language> Languages { get; set; }
    }
}