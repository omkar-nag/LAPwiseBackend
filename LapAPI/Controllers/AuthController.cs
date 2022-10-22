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
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthUserModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid User");
            }

            if (isValidUser(user))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LAPwiseSecretKey@123"));

                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5152",
                    audience: "http://localhost:5152",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        private bool isValidUser(AuthUserModel user)
        {
            System.Diagnostics.Debug.WriteLine("\n\n");

            System.Diagnostics.Debug.WriteLine(user.ToString());
            System.Diagnostics.Debug.WriteLine("\n\n");

            return true;
        }
    }
}
