using System.Collections.Generic;
using Languages.BL;
using Languages.BL.Domain;
using Languages.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers
{
    public class IdeController : Controller
    {
        private readonly IManager _manager;

        public IdeController(IManager manager)
        {
            _manager = manager;
        }

        // GET
        public IActionResult Index()
        {
            return View(_manager.GetAllIdes());
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var ide = _manager.GetIdeWithLanguages(id);
            IList<Language> lang = new List<Language>();
            foreach (var ideLanguage in ide.Languages)
            {
                lang.Add(ideLanguage.Language);
            }

            var ivm = new EditIdeViewModel(ide.Name, ide.Manufacturer, ide.ReleaseDate, ide.SupportedLanguages,
                ide.Price, lang);

            return View(new EditIdeWithLanguageViewModel(ivm, _manager.GetAllLanguages()));
        }
        
        [HttpPost]
        public IActionResult Edit(CreateIdeViewModel ivm)
        {
            return null;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CreateIdeViewModel ci)
        {
            if (!ModelState.IsValid) return View(ci);
            var ide = _manager.AddIde(ci.Name, ci.Manufacturer, ci.ReleaseDate, ci.SupportedLanguages, ci.Price);
            return RedirectToAction("Details", new {id = ide.Id});
        }

        public IActionResult Details(long id)
        {
            return View(_manager.GetIdeWithLanguages(id));
        }
    }
}