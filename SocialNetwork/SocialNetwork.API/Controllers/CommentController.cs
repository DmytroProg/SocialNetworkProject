using SocialNetwork.Core.DTOs;
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
        public async Task<ActionResult<CommentDTO>> GetCommentById ([FromRoute] int id)
        {
            try
            {
                return Ok(_mapper.Map<CommentDTO>(await _service.GetComment(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody]CreateCommentDTO createCommentDTO)
        {
            try
            {
                return Created(Url.Action(nameof(GetCommentById)), _mapper.Map<CommentDTO>(await _service.AddComment(_mapper.Map<Comment>(createCommentDTO))));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
