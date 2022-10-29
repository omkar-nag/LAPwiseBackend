using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace LapAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TempController : CustomControllerBase
    {
        private readonly LAPwiseDBContext _context;

        
        

        public TempController(LAPwiseDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        //[Authorize]
        public IActionResult GetSubTopicsForAllTopics(int userId)
        {
            //System.Diagnostics.Debug.WriteLine(GetLoggedInUserId());
            //Thread.Sleep(5000);

            var output = _context.AssessmentResults
                 .Join(_context.Assessments, x => x.AssessmentId, y => y.Id, (ar, a) => new { ar, a })
                 .Where(obj => obj.ar.UserId == userId);
                 //.OrderByDescending(obj => obj.ar.Score)
                 //.Select(obj => new CustomAssessment
                 //    { 
                 //        AssessmentId = obj.ar.AssessmentId, 
                 //        Title = obj.a.Title,
                 //        QuizId = obj.a.QuizId,
                 //        Topic = obj.a.Topics,
                 //        Score = obj.ar.Score
                 //    }
                 //).ToList();
            foreach(var topic in output)
            {
                Console.WriteLine(topic.ar.AssessmentId);
            }
            return Ok(output);
            
        }

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] Users users)
        //{
        //    _context.Users.Update(users);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
    }
}
