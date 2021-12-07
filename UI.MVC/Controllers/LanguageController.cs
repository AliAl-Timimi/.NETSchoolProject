using System;
using Languages.BL;
using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers
{
    public class LanguageController : Controller
    {
        private readonly IManager _manager;

        public LanguageController(IManager manager)
        {
            _manager = manager;
        }

        public IActionResult Details(long id)
        {
            return View(_manager.GetLanguageWithSoftware(id));
        }
    }
}