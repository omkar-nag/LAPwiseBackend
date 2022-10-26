using LapAPI.BusinessLayer.AssessmentsRepository;
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
        private IAssessmentsRepository _repository;

        public AssessmentsController(IAssessmentsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomAssessment>> GetAssessmentsAsync()
        {
            Thread.Sleep(2000);
            return await _repository.GetCustomAssessmentsAsync(GetLoggedInUserId());
        }

    }
}
