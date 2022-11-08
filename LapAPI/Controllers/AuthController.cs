using LapAPI.BusinessLayer.UserRepository;
using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LapAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUsersRepository _usersRepository;

        public AuthController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        private Users? getValidUser(AuthUserModel authUser)
        {
            return this._usersRepository.GetUserByUserNameAndPassword(authUser);
        }

        private bool isExistingUser(string userName)
        {
            return this._usersRepository.GetUserByUserName(userName) != null;
        }

        private Users? createUser(Users user)
        {
            try
            {
                _usersRepository.Insert(user);
                return user;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return null;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthUserModel authUser)
        {
            if (authUser == null) return BadRequest("Invalid User");

            var user = getValidUser(authUser);

            if (user == null) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LAPwiseSecretKey@123"));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5152",
                audience: "http://localhost:5152",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] Users requestUser)
        {

            if (this.isExistingUser(requestUser.UserName))
            {
                return Conflict(new { message = "Username already exists! Please try with a different username." });
            }

            requestUser.Password = BCrypt.Net.BCrypt.HashPassword(requestUser.Password);

            Users? user = createUser(requestUser);

            if (user != null) return Ok(new { message = "Registration successful!" });

            return BadRequest();
        }

    }
}
