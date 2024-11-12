using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace SocialNetwork.API.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        private readonly IMapper _mapper;

        public CommentController(ICommentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById ([FromRoute] int id)
        {
            try
            {
                var comment = await _service.GetComment(id);
                return Ok(comment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody]Comment bodyComment)
        {
            try
            {
                var comment = await _service.AddComment(bodyComment);
                return Created(Url.Action(nameof(GetCommentById)), comment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
