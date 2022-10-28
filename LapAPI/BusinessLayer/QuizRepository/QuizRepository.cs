using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.BusinessLayer.QuizRepository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly LAPwiseDBContext _dbContext;

        public QuizRepository(LAPwiseDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Quizzes?> GetById(int id)
        {
            return await _dbContext.Quizzes.Include(q => q.questions).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<AssessmentResults> InsertAssessmentResult(AssessmentResults result)
        {
            await _dbContext.AssessmentResults.AddAsync(result);
            _dbContext.SaveChanges();
            return result;
        }
    }
}
