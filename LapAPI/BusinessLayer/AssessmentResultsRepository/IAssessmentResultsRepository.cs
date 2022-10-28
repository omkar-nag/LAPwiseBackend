using Microsoft.AspNetCore.Mvc;
using LapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.BusinessLayer.AssessmentResultsRepository
{
    public interface IAssessmentResultsRepository
    {
        Task<List<AssessmentResults>> GetAssessmentResultsByUserId(int userId);

        Task<AssessmentResults> GetAssessmentResults(int AssessmentId);

        
    }
}
