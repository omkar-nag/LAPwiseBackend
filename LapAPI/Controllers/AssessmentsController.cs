﻿using LapAPI.BusinessLayer.AssessmentsRepository;
using LapAPI.BusinessLayer.QuizRepository;
using LapAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapAPI.Controllers
{

    [Route("api/assessments")]
    [ApiController]
    [Authorize]
    public class AssessmentsController : CustomControllerBase
    {
        private IAssessmentsRepository _assessmentsRepository;
        private IQuizRepository _quizRepository;

        public AssessmentsController(IAssessmentsRepository assessmentsRepository, IQuizRepository quizRepository)
        {
            _assessmentsRepository = assessmentsRepository;
            _quizRepository = quizRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomAssessment>> GetAssessmentsAsync()
        {
            return await _assessmentsRepository.GetCustomAssessmentsAsync(GetLoggedInUserId());
        }

        [HttpGet("quiz/{quizId:int}")]
        public async Task<Quizzes?> GetQuizzesAsync(int quizId)
        {
            return await _quizRepository.GetById(quizId);
        }

        [HttpPost("quiz/result")]
        public async Task<IActionResult> InsertAssessmentResultAsync([FromBody] AssessmentResults result)
        {
            await _quizRepository.InsertAssessmentResult(result);
            return Ok(new { message = "Success" });
        }


        [HttpGet("previous-results")]
        public async Task<IEnumerable<AssessmentResults>> GetPreviousResultsAsync()
        {
            return await _assessmentsRepository.GetAssessmentResultsAsync(GetLoggedInUserId());
        }

    }
}
