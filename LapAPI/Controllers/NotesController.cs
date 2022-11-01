using Microsoft.AspNetCore.Mvc;
using LapAPI.Models;
using LapAPI.BusinessLayer.NotesRepository;
using Microsoft.AspNetCore.Authorization;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class NotesController : ControllerBase
    {
        private INotesRepository _repository;

        public NotesController(INotesRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<Notes> GetNotes(int id)
        {

            return await _repository.GetNotes(id);
        }


        // GET: api/Notes/user/5

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<List<Notes>>> GetNotesByUserId(int userId)
        {
            var notes = await _repository.GetNotesByUserId(userId);
            if(notes == null)
            {
                return NotFound();
            }
            return notes;
        }


        // PUT: api/Notes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        public async Task<ActionResult<List<Notes>>> PutNotes(int userId, [FromBody] ICollection<Notes> notes)
        {
            ActionResult<List<Notes>> myNotes;
            try
            {
                 myNotes = await _repository.PutNotes(userId, notes);
            }
            catch (ItemUpdateException)
            {
                return BadRequest();
            }
            return myNotes;
        }

        // POST: api/Notes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Notes>>> PostNotes([FromBody]Notes notes)
        {
            var x = new Notes();
            x.Content = notes.Content;
            x.UserId = notes.UserId;
            x.Title = notes.Title;
            ActionResult<List<Notes>> myNotes;

            myNotes = await _repository.PostNotes(x);
            return myNotes;

        }

        // DELETE: api/Notes/5
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
