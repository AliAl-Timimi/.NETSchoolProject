using System.Linq;
using Languages.BL;
using Languages.UI.MVC.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers.api
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly IManager _mgr;

        public LanguagesController(IManager manager)
        {
            _mgr = manager;
        }

        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var language = _mgr.GetLanguageWithIdes(id);
            if (language == null)
                return NoContent();
            var lang = new LanguageDto(language);
            return Ok(lang);
        }

    }
}