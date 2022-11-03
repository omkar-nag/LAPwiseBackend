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

        public async Task<List<Notes>> GetNotesByUserId(int userId)
        {
            var notes = await _context.Notes.Where(note => note.UserId == userId).ToListAsync();

            return notes;
        }

        public Notes PutNotes(Notes note)
        {
            if (note.Id < 0)
            {
                Notes curr = new Notes
                {
                    Content = note.Content,
                    Title = note.Title,
                    UserId = note.UserId
                };
                _context.Notes.Add(curr);

                _context.SaveChanges();

                return curr;
            }
            _context.Attach(note);

            _context.Entry(note).State = EntityState.Modified;

            _context.SaveChanges();

            return note;

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


    }
}
