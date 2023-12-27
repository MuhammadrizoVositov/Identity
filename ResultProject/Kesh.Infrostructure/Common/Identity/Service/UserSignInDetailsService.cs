using AutoMapper;
using Kesh.Application.Common.Identity.Service;
using Kesh.Application.RequestContexts.Broker;
using Kesh.Domain.Entities;
using Kesh.Persistance.Repositories.Interfase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Identity.Service;
public class UserSignInDetailsService(
    IMapper mapper,
    IUserSignInDetailsRepository userSignInDetailsRepository,
    IRequestContextProvider requestContextProvider
) : IUserSignInDetailsService
{
    public async ValueTask<UserSignInDetails?> GetLastSignInDetailsAsync(Guid userId, bool asNoTracking, CancellationToken cancellationToken = default)
    {
        return await userSignInDetailsRepository.Get(signInDetails => signInDetails.UserId == userId, asNoTracking: asNoTracking)
            .OrderByDescending(signInDetails => signInDetails.CreatedTime)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async ValueTask RecordSignInAsync(bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var connectionInfo = requestContextProvider.GetRequestContext();
        var signInDetails = mapper.Map<UserSignInDetails>(connectionInfo);

        await userSignInDetailsRepository.CreateAsync(signInDetails, saveChanges, cancellationToken);
    }

    public async ValueTask<bool> ValidateSignInLocation(CancellationToken cancellationToken = default)
    {
        var connectionInfo = requestContextProvider.GetRequestContext();
        if (!connectionInfo.UserId.HasValue || connectionInfo.UserId.Value == Guid.Empty)
            throw new AuthenticationException("User is not authenticated.");

        var lastSignIn = await GetLastSignInDetailsAsync(connectionInfo.UserId.Value, true, cancellationToken);
        return lastSignIn is null || connectionInfo.IpAddress.Equals(lastSignIn.IpAddress);
    }
}
