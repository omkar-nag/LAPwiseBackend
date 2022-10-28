using LapAPI.Models;

namespace LapAPI.BusinessLayer.QuizRepository
{
    public interface IQuizRepository
    {
        Task<Quizzes?> GetById(int id);
        //Task<Quizzes> Insert(Quizzes quiz);
        //Task<Assessments> Update(Quizzes quiz);
        Task<AssessmentResults> InsertAssessmentResult(AssessmentResults result);
        //Task Delete(Quizzes quiz);
        //Task Save();
    }
}
