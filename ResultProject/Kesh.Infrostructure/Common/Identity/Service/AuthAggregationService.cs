using AutoMapper;


using Kesh.Application.Common.Identity.Models;
using Kesh.Application.Common.Identity.Service;
using Kesh.Domain.Entities;
using Kesh.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Identity.Service;
public class AuthAggregationService(IMapper mapper,
    IPasswordGeneratorService passwordGeneratorService,
    IPasswordHasherService passwordHasherService,
    IAccountAggregatorService accountAggregatorService,
    // IUserSignInDetailsService userSignInDetailsService,
    IAccessTokenGeneratorService accessTokenGeneratorService,
    IAccessTokenService accessTokenService,
    IUserService userService,
    IRoleService roleService
) : IAuthAggregationService
{
    

    public async ValueTask<AccessToken> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        var foundUser = await userService.GetByEmailAddressAsync(signInDetails.EmailAddress, cancellationToken: cancellationToken);

        // check user
        if (foundUser is null || !passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
            throw new AuthenticationException("Invalid email address or password.");

        if (!foundUser.IsEmailAddressVerified)
            throw new AuthenticationException("Email address is not verified.");

        // generate token

        // store token
        var accessToken = new AccessToken { UserId = foundUser.Id };

        var createdToken = await accessTokenService.CreateAsync(accessToken, cancellationToken: cancellationToken);
        var tokenValue = accessTokenGeneratorService.GetToken(foundUser);
        createdToken.Token = tokenValue.Token;
        createdToken.ExpiryTime = tokenValue.ExpiryTime;

        // update token
        return await accessTokenService.UpdateAsync(createdToken, cancellationToken: cancellationToken);
    }

    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        var foundUserId = await userService.GetIdByEmailAddressAsync(signUpDetails.EmailAddress, cancellationToken);

        if (foundUserId.HasValue)
            throw new EntityConflictException(foundUserId.Value, "User with this email address already exists.");

        // Hash password
        var user = mapper.Map<User>(signUpDetails);
        var password = signUpDetails.AutoGeneratePassword
        ? passwordGeneratorService.GeneratePassword()
            : passwordGeneratorService.GetValidatedPassword(signUpDetails.Password!, user);

        user.RoleId = await roleService.GetDefaultRoleId(cancellationToken);
        user.PasswordHash = passwordHasherService.HashPassword(password);

        // Create user
        return await accountAggregatorService.CreateUserAsync(user, cancellationToken);
    }
}
