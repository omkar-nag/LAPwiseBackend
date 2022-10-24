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

        public async Task<Notes> PutNotes(int id, Notes notes)
        {
            if (id != notes.Id)
            {
                throw new ItemUpdateException();
            }

            _context.Entry(notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
                {
                    throw new ItemNotFoundException();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult<Notes>(null);

        }


        public async Task<Notes> PostNotes(Notes notes)
        {
            _context.Notes.Add(notes);
            await _context.SaveChangesAsync();

            return await Task.FromResult<Notes>(null);
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
