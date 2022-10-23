using LapAPI.BusinessLayer.UserRepository;
using LapAPI.DataAccessLayer;
using LapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthUserModel authUser)
        {
            if (authUser == null)
            {
                return BadRequest("Invalid User");
            }

            var currUser = isValidUser(authUser);

            if (currUser.Id != 0)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LAPwiseSecretKey@123"));

                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, currUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, currUser.Id.ToString()),
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
            return Unauthorized();
        }

        private Users isValidUser(AuthUserModel authUser)
        {

            Users user = this._usersRepository.GetUserByUserNameAndPassword(authUser);

            return user ?? new Users();
        }
    }
}
