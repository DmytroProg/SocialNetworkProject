using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.DTOs;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using System.Xml.Linq;
namespace SocialNetwork.API.Controllers;
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UserController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    #region Get Methods
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers([FromQuery] string? name = null, [FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        try
        {
            if (name != null)
            {
                var namedUsers = await _service.GetUsersByName(name, skip, take);
                return Ok(namedUsers.Select(_mapper.Map<UserDTO>));
            }
            var users = await _service.GetUsers(skip, take);
            return Ok(users.Select(_mapper.Map<UserDTO>));
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserbyId([FromRoute] int id)
    {
        try
        {
            return Ok(_mapper.Map<UserDTO>(await _service.GetUserById(id)));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
    #region Post Methods
    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody]CreateUserDTO createUserDto)
    {
        try
        {
            var createdUser = await _service.SignUp(_mapper.Map<User>(createUserDto));
            return Created(Url.Action(nameof(GetUserbyId), new { id = createdUser.Id }), _mapper.Map<UserDTO>(createdUser));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
    #region Put Methods
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDTO>> UpdateUser([FromRoute] int id, [FromBody]CreateUserDTO createUserDto)
    {
        try
        {
            var updatedUser = await _service.UpdateUser(id, _mapper.Map<User>(createUserDto));
            return Created(Url.Action(nameof(GetUserbyId), new { id = updatedUser.Id }), _mapper.Map<UserDTO>(updatedUser));
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
    #region Patch Methods
    [HttpPatch("login")]
    public async Task<ActionResult<UserDTO>> LogInUser([FromRoute]int id)
    {
        try
        {
            return Ok( _mapper.Map<UserDTO>(await _service.LogIn(id)));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPatch("logout")]
    public async Task<ActionResult> LogOutUser([FromRoute] int id)
    {
        try
        {
            await _service.LogOut(id);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
}