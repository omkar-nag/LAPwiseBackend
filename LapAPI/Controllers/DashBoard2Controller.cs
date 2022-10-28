﻿using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoard2Controller : ControllerBase
    {
        private readonly LAPwiseDBContext _lAPwiseDBContext;
        public DashBoard2Controller(LAPwiseDBContext lAPwiseDBContext)
        {
            _lAPwiseDBContext = lAPwiseDBContext;
        }
        [HttpGet]
        public IEnumerable<Quizzes> GetQuizzes()
        {
            return _lAPwiseDBContext.Quizzes;
        }
    }
}
