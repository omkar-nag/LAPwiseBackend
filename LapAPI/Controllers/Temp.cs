using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public Quizzes GetSubTopicsForAllTopics()
        {
            System.Diagnostics.Debug.WriteLine(GetLoggedInUserId());
            return new Quizzes();
        }
    }
}
