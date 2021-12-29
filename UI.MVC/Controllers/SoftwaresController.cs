using System.Linq;
using Languages.BL;
using Languages.UI.MVC.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwaresController : ControllerBase
    {
        private readonly IManager _mgr;

        public SoftwaresController(IManager manager)
        {
            _mgr = manager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var software = _mgr.GetAllSoftware();
            if (software == null || !software.Any())
            {
                return NoContent();
            }
            return Ok(software);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetOne(long id)
        {
            var software = _mgr.GetSoftware(id);
            if (software == null)
            {
                return NoContent();
            }
            return Ok(software);
        }
        
        [HttpPost]
        public IActionResult Post(SoftwareDto s)
        {
            var software = _mgr.AddSoftware(s.Name, s.Description);
            if (software == null) return NoContent();

            var dto = new SoftwareDto()
            {
                Id = software.Id,
                Name = software.Name,
                Description = software.Description
            };
            
            return CreatedAtAction("GetOne", new {id = dto.Id}, dto);
        }

        /*
        [HttpGet("{id}")]
        public IActionResult GetOne(long id)
        {
            var software = _mgr.GetSoftware(id);
            if (software == null) return NotFound();
            return Ok(software);
        }
        */
    }
}