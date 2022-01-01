using System.Collections.Generic;
using Languages.BL;
using Languages.UI.MVC.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers.api
{
    [ApiController]
    [Route("/api/[controller]")]
    public class IdesController : ControllerBase
    {
        private IManager _mgr;

        public IdesController(IManager mgr)
        {
            _mgr = mgr;
        }

        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var ides = _mgr.GetNonLinkedIdes(id);
            if (ides == null)
                return Ok(new List<IdeWithoutLangDto>());
            return Ok(ides);
        }
        
    }
}