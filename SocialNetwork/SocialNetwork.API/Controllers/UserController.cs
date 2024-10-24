using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // localhost:port/api/users?take={take}&skip={skip}
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers([FromQuery]int skip = 0, [FromQuery]int take = 20)
    {
        return Ok(_userService.GetAllUsers(skip, take));
    }

    [HttpPost]
    public async Task<ActionResult<User>> AddUser([FromBody]User user)
    {
        // validation
        
        await _userService.AddUser(user);
        return Created($"users/{1}", user);
    }

    //localhost:port/api/users/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute]int id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }

    //localhost:port/api/users/{id}/
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser([FromRoute]int id, [FromBody]User user)
    {
        user.Id = id;
        await _userService.UpdateUser(user);

        return Ok(user);
    }
}