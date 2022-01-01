using Languages.BL;
using Languages.UI.MVC.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdeLanguagesController : ControllerBase
    {
        private readonly IManager _mgr;

        public IdeLanguagesController(IManager mgr)
        {
            _mgr = mgr;
        }

        [HttpPost]
        public IActionResult Post([FromBody] IdeLanguageDto dto)
        {
            var ideAdded = _mgr.AddLanguageToIde(dto.IdeId, dto.LangId, dto.PopOrder);
            if (ideAdded == null)
                return BadRequest("Something went wrong when adding the ide");
            return Ok();
        }
    }
}