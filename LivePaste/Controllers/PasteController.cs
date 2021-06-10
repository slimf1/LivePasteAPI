using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Repository;
using DAL.Models;


namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/pastes")]
    public class PasteController : ControllerBase
    {
        private IPasteRepository _pasteRepository;

        public PasteController()
        {
            _pasteRepository = new PasteRepository(new LivePasteContext());
        }

        [HttpGet]
        public IEnumerable<Paste> Get()
        {
            return _pasteRepository.GetPastes();
        }

        [HttpGet("language/{language}")]
        public IEnumerable<Paste> Get(string language)
        {
            return _pasteRepository.GetPastes(language);
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
