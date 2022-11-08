using Microsoft.AspNetCore.Mvc;
using LapAPI.Models;
using LapAPI.BusinessLayer.NotesRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : CustomControllerBase
    {
        private readonly INotesRepository _repository;
        private readonly ILogger<Notes> _logger;

        public NotesController(INotesRepository repository, ILogger<Notes> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Notes>>> GetNotesByUserId()
        {
            var userId = GetLoggedInUserId();

            try
            {

                var notes = await _repository.GetNotesByUserId(userId);

                _logger.LogInformation($"{DateTime.UtcNow.ToString()} Fetching notes for userId: {userId}");

                return Ok(notes);

            }
            catch (ItemNotFoundException ex)
            {
                _logger.LogInformation($"{DateTime.UtcNow.ToString()} ItemNotFoundException: Database exception occured while Fetching notes for userId: {userId}");

                return NotFound(ex);

            }


        }

        [HttpPost]

        public ActionResult<Notes> PostNotes([FromBody] Notes note)
        {

            try
            {

                var postedNote = _repository.PostNotes(note);

                _logger.LogInformation($"{DateTime.UtcNow.ToString()} Note with noteId: {note.Id} posted");

                return Ok(postedNote);

            }
            catch(InvalidOperationException ex)
            {

                _logger.LogError($"{DateTime.UtcNow.ToString()}InvalidOperationException: @Note with noteId: {note.Id}\n {ex.Message}");

                return BadRequest();

            }
            catch(DbUpdateException ex)
            {

                _logger.LogError($"{DateTime.UtcNow.ToString()}DbUpdateException: Note with noteId: {note.Id} cannot be added because of DbUpdateException: {ex.Message}");

                return StatusCode(500, ex);

            }
        }


        [HttpPut]
        public ActionResult<Notes> PutNotes([FromBody] Notes notes)
        {

            try
            {

                var updatedNote = _repository.PutNotes(notes);

                _logger.LogInformation($"{DateTime.UtcNow.ToString()} Note with noteId: {updatedNote.Id} updated");

                return Ok(updatedNote);

            }
            catch(ItemNotFoundException ex)
            {

                _logger.LogError($"{DateTime.UtcNow.ToString()} ItemNotFoundException: Note with ID = {notes.Id} does not exist ");


                return NotFound(ex);

            }
            catch (DbUpdateException ex) 
            { 
                
                return StatusCode(500, ex);
            
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {

            try
            {

                _logger.LogInformation($"{DateTime.UtcNow.ToString()} Deleted Note with noteId: {id}");

                await _repository.DeleteNotes(id);

            }
            catch (ItemNotFoundException ex)
            {

                _logger.LogError($"{DateTime.UtcNow.ToString()} ItemNotFoundException: Note with noteId: {id} does not exist");

                return NotFound(ex);

            }
            catch(DbUpdateException ex)
            {

                _logger.LogError($"{DateTime.UtcNow.ToString()} DbUpdateException: Note with noteId: {id} cannot be deleted ");

                return StatusCode(500, ex);

            }
            return Ok();
        }

    }
}
