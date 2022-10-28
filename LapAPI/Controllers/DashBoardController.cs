using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly LAPwiseDBContext _lAPwiseDBContext;
        public DashBoardController(LAPwiseDBContext lAPwiseDBContext)
        {
            _lAPwiseDBContext = lAPwiseDBContext;
        }
        [HttpGet]
        public IEnumerable<Topics> GetTopics()
        {
            return _lAPwiseDBContext.Topics;
        }
    }
}
