using Microsoft.AspNetCore.Mvc;
using LapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.BusinessLayer.NotesRepository
{
    public interface INotesRepository 
    {
        Task<List<Notes>> GetNotesByUserId(int userId);

        Notes PutNotes(Notes notes);

        Task<Notes> DeleteNotes(int id);

        Notes PostNotes(Notes notes);

    }
}
