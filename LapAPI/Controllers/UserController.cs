﻿
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
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
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
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] Users user)
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

        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordObject passwordObj)
        {
            Users user = await _usersRepository.GetById(GetLoggedInUserId());

            bool isValid = BCrypt.Net.BCrypt.Verify(passwordObj.OldPassword, user.Password);

            if (!isValid) return Unauthorized();

            user.Password = BCrypt.Net.BCrypt.HashPassword(passwordObj.NewPassword);

            await _usersRepository.Update(user.Id, user);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<Users> GetUser([FromRoute] int id)
        {
            return await _usersRepository.GetById(id);
        }
    }
}