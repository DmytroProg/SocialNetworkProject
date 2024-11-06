using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.API.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        public PostController(IPostService service)
        {
            _service = service;
        }
        [HttpGet("filtered/{isFildered}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromRoute] bool isFiltered, [FromQuery] int skip, [FromQuery] int take)
        {
            try
            {
                return Ok(await _service.GetPosts(isFiltered, skip, take));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById([FromRoute] int id)
        {
            try
            {
                return Ok(await _service.GetPostById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
        {
            try
            {
                return Created(Url.Action(nameof(GetPostById)), await _service.CreatePost(post));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
