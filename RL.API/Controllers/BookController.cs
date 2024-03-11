using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet]
        [Authorized]
        [ProducesResponseType(typeof(HashSet<BookResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _bookServices.GetAllAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("Author/{name}")]
        [Authorized]
        [ProducesResponseType(typeof(HashSet<BookResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByAuthor([FromRoute] string name)
        {
            string[] author = name.Split(' ');

            var result = await _bookServices.GetByAsync(c=> author.Contains(c.FirstName) && author.Contains(c.LastName));
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [Authorized]
        [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _bookServices.GetByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("Add")]
        [Authorized]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostBook([FromBody] BookRequest user)
        {
            var result = await _bookServices.PostAsync(user);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("Update")]
        [Authorized]
        [ProducesResponseType(typeof(OperationResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutBook([FromBody] BookUpdateRequest user)
        {
            var result = await _bookServices.PutAsync(user);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
