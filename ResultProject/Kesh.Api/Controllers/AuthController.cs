using Kesh.Application.Common.Identity.Models;
using Kesh.Application.Common.Identity.Service;
using Kesh.Application.Common.Queriyng;
using Microsoft.AspNetCore.Mvc;

namespace Kesh.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthAggregationService authAggregationService) : ControllerBase
{

    [HttpPost("sign-up")]
    public async ValueTask<IActionResult> SignUp([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken)
    {
        var result = await authAggregationService.SignUpAsync(signUpDetails, cancellationToken);
        return result ? Ok() : BadRequest();
    }

    // [HttpPost("sign-in")]
    // public async ValueTask<IActionResult> SignIn([FromBody] SignInDetails signInDetails, CancellationToken cancellationToken)
    // {
    //     var result = await authAggregationService.SignInAsync(signInDetails, cancellationToken);
    //     return Ok(result);
    // }

    [HttpGet("roles")]
    public async ValueTask<IActionResult> GetRoles(
        [FromQuery] FilterPagination paginationOptions,
        [FromServices] IRoleService roleService,
        CancellationToken cancellationToken
    )
    {
        var result = await roleService.GetByFilterAsync(paginationOptions, true, cancellationToken);
        return Ok(result);
    }
}