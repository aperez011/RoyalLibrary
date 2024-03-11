using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthenticationController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(HashSet<AuthResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] AuthRequest login)
        {
            var result = await _authServices.Login(login);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("Logout")]
        [Authorized]
        [ProducesResponseType(typeof(OperationResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Logout()
        {
            int sessionId = 0;
            var result = await _authServices.LogOut(sessionId);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
