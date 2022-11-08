using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace LapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public int GetLoggedInUserId()
        {
            string userId;
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                try
                {
                    userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                }
                catch (ArgumentNullException)
                {
                    userId = "-1";
                }

                return int.Parse(userId);
            }

            throw new AuthenticationException();
        }
    }
}
