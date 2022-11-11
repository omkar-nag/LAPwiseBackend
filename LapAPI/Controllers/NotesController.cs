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

        public NotesController(INotesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Notes>>> GetNotesByUserId()
        {
            var userId = GetLoggedInUserId();

            try
            {

                var notes = await _repository.GetNotesByUserId(userId);

                return Ok(notes);

            }
            catch (ItemNotFoundException ex)
            {
                return NotFound(ex);

            }


        }

        [HttpPost]

        public ActionResult<Notes> PostNotes([FromBody] Notes note)
        {

            try
            {

                var postedNote = _repository.PostNotes(note);

                return Ok(postedNote);

            }
            catch(InvalidOperationException ex)
            {

                return BadRequest();

            }
            catch(DbUpdateException ex)
            {

                return StatusCode(500, ex);

            }
        }


        [HttpPut]
        public ActionResult<Notes> PutNotes([FromBody] Notes notes)
        {

            try
            {

                var updatedNote = _repository.PutNotes(notes);

                return Ok(updatedNote);

            }
            catch(ItemNotFoundException ex)
            {

                return NotFound(ex);

            }
            catch (DbUpdateException ex) 
            { 
                
                return StatusCode(500, ex);
            
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Notes>> DeleteNotes(int id)
        {

            try
            {

                 await _repository.DeleteNotes(id);

            }
            catch (ItemNotFoundException ex)
            {

                return NotFound(ex);

            }
            catch(DbUpdateException ex)
            {

                return StatusCode(500, ex);

            }
            return Ok(new {message="success"});
        }

    }
}
