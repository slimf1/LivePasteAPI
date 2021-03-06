using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Repository;
using DAL.Models;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/pastes")]
    [Produces("application/json")]
    public class PasteController : ControllerBase
    {
        private IPasteRepository _pasteRepository;
        private readonly IConfiguration _configuration;

        public PasteController(IConfiguration configuration)
        {
            _pasteRepository = new PasteRepository(new LivePasteContext(configuration));
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Paste> Get(string language = null)
        {
            return _pasteRepository.GetPastesFromLanguage(language);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                BadRequest();

            Paste paste = _pasteRepository.GetPasteByID(id);

            if (paste == null)
                return NotFound();
            
            return Ok(paste);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Paste paste)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                paste.CreationDate = DateTime.Now;
                _pasteRepository.InsertPaste(paste);
                _pasteRepository.Save();
                return CreatedAtAction("POST", new { id = paste.PasteID }, paste);
            } catch (Exception e)
            {
                Console.WriteLine($"POST EXCEPTION: {e.Message}");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]    
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                BadRequest();
            
            if (_pasteRepository.DeletePaste(id))
            {
                _pasteRepository.Save();
                return Ok();
            }

            return NotFound();
        }
    }
}
