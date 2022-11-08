using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        private readonly LAPwiseDBContext _context;
        public NotesRepository(LAPwiseDBContext context)
        {
            _context = context;
        }

        public async Task<List<Notes>> GetNotesByUserId(int userId)
        {

            try
            {
                var notes = await _context.Notes.Where(note => note.UserId == userId).ToListAsync();

                return notes;
            }
            catch (ItemNotFoundException ex)
            {

                throw ex;
            }
        }

        public Notes PostNotes(Notes note)
        {
            if (note.Id > 0)
            {
                throw new InvalidOperationException();
            }
            try
            {
                Notes postedNote = new Notes
                {
                    Title = note.Title,
                    Content = note.Content,
                    UserId = note.UserId,
                };

                _context.Notes.Add(postedNote);

                _context.SaveChanges();

                return postedNote;
            }
            catch(DbUpdateException ex)
            {
                throw ex;
            }
        }

        public Notes PutNotes(Notes note)
        {
            if (!NoteExists(note.Id))
            {
                throw new ItemNotFoundException($"Note with ID = {note.Id} and Title = '{note.Title}' does not exist");
            }

            try
            {
                _context.Attach(note);

                _context.Entry(note).State = EntityState.Modified;

                _context.SaveChanges();

                return note;
            }
            catch (DbUpdateException ex)
            {

                throw ex;

            }
        }

        public async Task<Notes> DeleteNotes(int id)
        {
            if (!NoteExists(id))
            {

                throw new ItemNotFoundException();

            }
            try
            {
                var notes = await _context.Notes.FindAsync(id);

                _context.Notes.Remove(notes);
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                throw ex;

            }

            return await Task.FromResult<Notes>(null);
        }

        public bool NoteExists(int id)
        {
            return _context.Notes.Where((note) => note.Id == id).Any();
        }

    }
}
