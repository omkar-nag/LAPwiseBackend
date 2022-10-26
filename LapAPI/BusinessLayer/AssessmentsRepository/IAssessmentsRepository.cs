using LapAPI.Models;

namespace LapAPI.BusinessLayer.AssessmentsRepository
{
    public interface IAssessmentsRepository
    {
        Task<IEnumerable<Assessments>> GetAll();
        Task<IEnumerable<CustomAssessment>> GetCustomAssessmentsAsync(int userId);
        Task<Assessments?> GetById(int Id);
        Task<Assessments> Insert(Assessments assessment);
        Task<Assessments> Update(Assessments assessment);
        Task Delete(Assessments assessment);
        Task Save();
    }
}
