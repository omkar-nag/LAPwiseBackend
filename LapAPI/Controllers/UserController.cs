
using LapAPI.BusinessLayer.UserRepository;
using LapAPI.Controllers;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LapAPI.BusinessLayer.NotesRepository;
using NuGet.Protocol.Core.Types;

namespace LapAPI.Controllers
{
    public class PasswordObject
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomControllerBase
    {
        private IUsersRepository _usersRepository;

        public UserController(IUsersRepository repository)
        {
            _usersRepository = repository;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> PutUser([FromRoute] int id, [FromBody] Users user)
        {
            Users updateStatus = null;

            try
            {
                updateStatus = await  _usersRepository.Update(id, user);
            }
            catch (ItemUpdateException ex)
            {
                return BadRequest(ex);
            }
            return Ok(updateStatus);
        }

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordObject passwordObj)
        {
            Users? user =  await _usersRepository.GetById(GetLoggedInUserId());

            if (user == null) return BadRequest();

            bool isValid = BCrypt.Net.BCrypt.Verify(passwordObj.OldPassword, user.Password);

            if (!isValid) return Unauthorized();

            user.Password = BCrypt.Net.BCrypt.HashPassword(passwordObj.NewPassword);

            await _usersRepository.Update(user.Id, user);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser([FromRoute] int id)
        {
            try
            {
                return Ok(await _usersRepository.GetById(id));
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound(ex);
            }
        }
    }
}