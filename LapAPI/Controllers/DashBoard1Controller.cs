using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoard1Controller : ControllerBase
    {
        private readonly LAPwiseDBContext _lAPwiseDBContext;
        public DashBoard1Controller(LAPwiseDBContext lAPwiseDBContext)
        {
            _lAPwiseDBContext = lAPwiseDBContext;
        }
        [HttpGet]
        public IEnumerable<SubTopics> GetSubTopics()
        {
            return _lAPwiseDBContext.SubTopics;
        }

        [HttpGet("{id}", Name = "GetSubTopicByTopicId")]
        public IEnumerable<SubTopics> GetSubTopicByTopicId(int id)
        {
            var query = _lAPwiseDBContext.SubTopics.Where(x => x.TopicsId == id);
            return query;
        }
    }
}
