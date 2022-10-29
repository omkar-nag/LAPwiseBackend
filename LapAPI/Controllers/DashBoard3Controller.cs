using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoard3Controller : ControllerBase
    {
        private readonly LAPwiseDBContext _lAPwiseDBContext;
        public DashBoard3Controller(LAPwiseDBContext lAPwiseDBContext)
        {
            this._lAPwiseDBContext = lAPwiseDBContext;
        }
        [HttpGet]
        public IEnumerable<Questions> GetQuestions()
        {
            return _lAPwiseDBContext.Questions;
        }
        [HttpGet("{title}", Name = "GetQuestionsByTitle")]
        public IEnumerable<Questions> GetQuestionsByTitle(string title)
        {
            var q = _lAPwiseDBContext.Quizzes.Where(x => x.Title == title).Select(x => x.Id).FirstOrDefault();
            var query = _lAPwiseDBContext.Questions.Where(x => x.QuizId == q);
            return query;
        }
    }
}
