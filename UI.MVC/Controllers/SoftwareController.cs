using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers
{
    public class SoftwareController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}