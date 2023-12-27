using Kesh.Domain.Broker;
using Kesh.Domain.Constants;
using Kesh.Infrostructure.Common.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.RequestContexts.Broker;
public class RequestUserContextProvider(
    IHttpContextAccessor httpContextAccessor,
    IOptions<RequestUserContextSettings> requestUserContextSettings
) : IRequestUserContextProvider
{
    private readonly RequestUserContextSettings _requestUserContextSettings = requestUserContextSettings.Value;


    public Guid GetUserIdAsync(CancellationToken cancellationToken = default)
    {
        var httpContext = httpContextAccessor.HttpContext!;
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)?.Value;
        return userIdClaim is not null ? Guid.Parse(userIdClaim) : _requestUserContextSettings.SystemUserId;
    }
}
