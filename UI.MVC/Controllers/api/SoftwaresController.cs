using System.Linq;
using Languages.BL;
using Languages.BL.Domain;
using Languages.UI.MVC.Models.dto;
using Microsoft.AspNetCore.Mvc;

namespace Languages.UI.MVC.Controllers.api
{
    [ApiController]
    [Route("/api/[controller]")]
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
                return NoContent();
            return Ok(software);
        }
        
        [HttpGet("{id:long}")]
        public IActionResult Get(long id)
        {
            var software = _mgr.GetSoftware(id);
            if (software == null)
                return NoContent();
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
            
            return CreatedAtAction("Get", new {id = dto.Id}, dto);
        }
        
        [HttpPut("{id:long}")]
        public IActionResult Put(long id, [FromBody] Software software)
        {
            if (id != software.Id)
                return BadRequest("Ids should match");
            if (software.Name.Length < 3)
                return BadRequest("Name should be at least 3 characters long");
            _mgr.ChangeSoftware(software);
            return NoContent();
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