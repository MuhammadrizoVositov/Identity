using Kesh.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kesh.Application.RequestContexts.Broker;
using Kesh.Application.RequestContexts.Models;
namespace Kesh.Infrostructure.Common.RequestContexts.Broker;
public class RequestContextProvider(IHttpContextAccessor httpContextAccessor) : IRequestContextProvider
{
    public RequestContext GetRequestContext()
    {
        var httpContext = httpContextAccessor.HttpContext!;
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)?.Value;

        var requestContext = new RequestContext
        {
            UserId = userIdClaim is not null ? Guid.Parse(userIdClaim) : default,
            IpAddress = httpContext.Connection.RemoteIpAddress!.ToString(),
            UserAgent = httpContext.Request.Headers[HeaderNames.UserAgent].ToString()
        };

        return requestContext;
    }
}
