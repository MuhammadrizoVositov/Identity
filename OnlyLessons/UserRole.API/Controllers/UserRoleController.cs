using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRole.Application.Services;
using UserRole.Domain.Common.Enums;
using UserRole.Domain.Entites;

namespace UserRole.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly IAuthService _authService;

    public UserRoleController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ValueTask<IActionResult> CreatAsync([FromBody] User user)
    {
        return new(Ok(_authService.RegisterAsync(user)));
    }
    [HttpPost("login")]
    public ValueTask<IActionResult> LoginAsync([FromBody] User user)
    {
        return new(Ok(_authService.LoginAsync(user)));
    }
    [HttpPut("{id:guid}")]
    public ValueTask<IActionResult> GiveRole([FromRoute] Guid userId, [FromQuery] Role role)
    {
        return new (Ok(_authService.GiveRole(userId, role)));
    }

}
