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

}