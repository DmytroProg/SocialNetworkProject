using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using System.Xml.Linq;
namespace SocialNetwork.API.Controllers;
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }
    #region Get Methods
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        try
        {
            return Ok( await _service.GetUsers(skip, take));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("id")]
    public async Task<ActionResult<User>> GetUserbyId([FromRoute] int id)
    {
        try
        {
            return Ok(await _service.GetUserById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("name")]
    public async Task<ActionResult<User>> GetUserbyName([FromRoute] string name)
    {
        try
        {
            return Ok(await _service.GetUserByName(name));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
    #region Post Methods
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody]User user)
    {
        try
        {
            var createdUser = await _service.SignUp(user);
            return Created(Url.Action(nameof(GetUserbyId), new { id = createdUser.Id }), createdUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
    #region Put Methods
    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser(int id, User user)
    {
        try
        {
            var updatedUser = await _service.UpdateUser(id, user);
            return Created(Url.Action(nameof(GetUserbyId), new { id = updatedUser.Id }), updatedUser);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
    #region Patch Methods
    [HttpPatch("log_in/user")]
    public async Task<ActionResult<User>> LogInUser([FromRoute]int id)
    {
        try
        {
            return Ok( await _service.LogIn(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPatch("log_out/user")]
    public async Task<ActionResult> LogOutUser([FromRoute] int id)
    {
        try
        {
            await _service.LogOut(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    #endregion
}