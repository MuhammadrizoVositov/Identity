using Kesh.Application.Common.Verification.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kesh.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VerificationsController : ControllerBase
{
    [HttpPut("{code}")]
    public async ValueTask<IActionResult> Verify(
    [FromRoute] string code,
    [FromServices] IVerificationProcessingService verificationProcessingService,
    CancellationToken cancellationToken
)
    {
        var result = await verificationProcessingService.Verify(code, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}
