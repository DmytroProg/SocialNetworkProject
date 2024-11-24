using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.DTOs;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.API.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IMapper _mapper;
        public PostController(IPostService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts([FromQuery] bool isFiltered, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var posts = await _service.GetPosts(isFiltered, skip, take);
                return Ok(posts.Select(_mapper.Map<PostDTO>));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostById([FromRoute] int id)
        {
            try
            {
                return Ok(_mapper.Map<PostDTO>(await _service.GetPostById(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO createPostDto)
        {
            try
            {
                return Created(Url.Action(nameof(GetPostById)),_mapper.Map<PostDTO>(await _service.CreatePost(_mapper.Map<Post>(createPostDto))));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
