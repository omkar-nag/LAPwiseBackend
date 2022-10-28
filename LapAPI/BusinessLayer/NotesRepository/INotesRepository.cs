using Microsoft.AspNetCore.Mvc;
using LapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.BusinessLayer.NotesRepository
{
    public interface INotesRepository 
    {
        Task<List<Notes>> GetNotesByUserId(int userId);
        Task<Notes> GetNotes(int id);
        Task<ICollection<Notes>> PutNotes(int userId,ICollection<Notes> notes);

        Task<Notes> PostNotes(Notes notes);
        Task<Notes> DeleteNotes(int id);

    }
}
