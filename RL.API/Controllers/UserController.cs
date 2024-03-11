using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [Authorized]
        [ProducesResponseType(typeof(HashSet<UserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _userServices.GetAllAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [Authorized]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _userServices.GetByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("Add")]
        [Authorized]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostUser([FromBody] UserRequest user)
        {
            var result = await _userServices.PostAsync(user);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("Update")]
        [Authorized]
        [ProducesResponseType(typeof(OperationResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutUser([FromBody] UserUpdateRequest user)
        {
            var result = await _userServices.PutAsync(user);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
