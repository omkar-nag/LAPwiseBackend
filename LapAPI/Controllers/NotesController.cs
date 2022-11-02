using Microsoft.AspNetCore.Mvc;
using LapAPI.Models;
using LapAPI.BusinessLayer.NotesRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]                     
    public class NotesController : CustomControllerBase
    {
        private INotesRepository _repository;

        public NotesController(INotesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Notes>>> GetNotesByUserId()
        {
            var userId = GetLoggedInUserId();

            var notes = await _repository.GetNotesByUserId(userId);

            return notes==null?NotFound():notes;

        }

        [HttpPut]
        public ActionResult<Notes> PutNotes([FromBody] Notes notes)
        {

            Notes tempNote;
            try
            {

                tempNote = _repository.PutNotes(notes);

            }
            catch (ItemUpdateException)
            {

                return BadRequest();

            }
            return Ok(tempNote);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {

            try
            {

                await _repository.DeleteNotes(id);

            }
            catch (ItemNotFoundException)
            {

                return NotFound();

            }
            return Ok();
        }

    }
}
