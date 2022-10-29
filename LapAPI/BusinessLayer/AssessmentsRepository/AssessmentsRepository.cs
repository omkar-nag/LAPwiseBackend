﻿using LapAPI.DataAccessLayer;
using LapAPI.Models;

using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace LapAPI.BusinessLayer.AssessmentsRepository
{
    public class AssessmentsRepository : IAssessmentsRepository
    {
        private readonly LAPwiseDBContext _dbContext;
        public AssessmentsRepository(LAPwiseDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<Assessments>> GetAll()
        {
            return await _dbContext.Assessments.ToListAsync();
        }
        public async Task<IEnumerable<CustomAssessment>> GetCustomAssessmentsAsync(int userId)
        {

            List<CustomAssessment> assessments = new List<CustomAssessment>();

            foreach (var item in _dbContext.Assessments.Include(a => a.Topics).Include(a => a.Quizzes))
            {
                var bestResult = _dbContext.AssessmentResults.Where(a => a.AssessmentId == item.Id && a.UserId == userId).OrderByDescending(a => a.Score).FirstOrDefault();
                assessments.Add(new CustomAssessment
                {
                    Assessment = item,
                    Score = bestResult != null ? bestResult.Score : 0,
                });
            }

            //var query = from a in _dbContext.Set<Assessments>()
            //            join b in _dbContext.Set<AssessmentResults>()
            //                on a.Id equals b.AssessmentId into grouping

            //            from p in grouping.DefaultIfEmpty()
            //            select new CustomAssessment
            //            {
            //                AssessmentId = a.Id,
            //                Title = a.Title,
            //                QuizId = a.QuizId,
            //                Topic = a.Topics,
            //                Score = p == null ? 0 : p.Score 
            //            };

            return await Task.FromResult(assessments);
        }

        public async Task<Assessments?> GetById(int Id)
        {
            return await _dbContext.Assessments.FindAsync(Id);
        }

        public async Task<Assessments> Insert(Assessments assessment)
        {
            await _dbContext.Assessments.AddAsync(assessment);
            await this.Save();
            return assessment;
        }

        public async Task<Assessments> Update(Assessments assessment)
        {
            await _dbContext.Assessments.AddAsync(assessment);
            await this.Save();
            return assessment;
        }

        public async Task Delete(Assessments assessment)
        {
            _dbContext.Assessments.Remove(assessment);
            await this.Save();
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}