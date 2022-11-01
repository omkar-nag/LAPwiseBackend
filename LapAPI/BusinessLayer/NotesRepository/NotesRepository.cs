using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.BusinessLayer.NotesRepository
{
    public class ItemUpdateException : Exception
    {
        public ItemUpdateException() { }

        public ItemUpdateException(string message) : base(message) { }

        public ItemUpdateException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() { }

        public ItemNotFoundException(string message) : base(message) { }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class NotesRepository : INotesRepository
    {
        private LAPwiseDBContext _context;
        public NotesRepository(LAPwiseDBContext context)
        {
            _context = context;
        }

        public async Task<Notes> GetNotes(int id)
        {
            var notes = await _context.Notes.FindAsync(id);

            return notes;
        }

        public async Task<List<Notes>> GetNotesByUserId(int userId)
        {
            var notes = await  _context.Notes.Where(note => note.UserId == userId).ToListAsync();
            return notes;
        }

        public async Task<ActionResult<List<Notes>>> PutNotes(int userId, ICollection<Notes> notes)
        {

            foreach (var note in notes)
            {
                if (note.Id == -1)
                {
                    Notes curr = new Notes
                    {
                        Content = note.Content,
                        Title = note.Title,
                        UserId = note.UserId
                    };
                    _context.Notes.Add(curr);
                    await _context.SaveChangesAsync();
                    continue;
                }
                _context.Attach(note);
                var n = _context.Notes.Update(note);
                _context.Entry(note).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            List<Notes> myNotes = new List<Notes>();
            myNotes = _context.Notes.Where(note => note.UserId == userId).ToList();

            return await Task.FromResult<List<Notes>>(myNotes);

        }


        public async Task<ActionResult<List<Notes>>> PostNotes(Notes notes)
        {
            _context.Notes.Add(notes);
            await _context.SaveChangesAsync();

            List<Notes> myNotes = new List<Notes>();
            myNotes = _context.Notes.Where(note => note.UserId == notes.UserId).ToList();

            return await Task.FromResult<List<Notes>>(myNotes);
        }

        public async Task<Notes> DeleteNotes(int id)
        {
            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                throw new ItemNotFoundException();
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return await Task.FromResult<Notes>(null);
        }


        private bool NotesExists(int id)
        {
            return _context.Notes.Any(note => note.Id == id);
        }
    }
}
