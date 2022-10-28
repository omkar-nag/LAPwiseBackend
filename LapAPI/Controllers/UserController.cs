
using LapAPI.BusinessLayer.UserRepository;
using LapAPI.Controllers;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LapAPI.BusinessLayer.NotesRepository;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUsersRepository _usersRepository;

        public UserController(IUsersRepository repository)
        {
            _usersRepository = repository;
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute]int id, [FromBody] Users user)
        {
            try
            {
                var updateStatus = await _usersRepository.Update(id, user);
            }
            catch (ItemUpdateException)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}