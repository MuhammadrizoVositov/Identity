using Kesh.Domain.Entities;
using Kesh.Persistance.DataCantext;
using Kesh.Persistance.Repositories.Interfase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Persistance.Repositories;
public class UserInfoVerificationCodeRepository(IdentityDbContext identityDbContext) :
    EntityRepositoryBase<UserInfoVerificationCode, IdentityDbContext>(identityDbContext), IUserInfoVerificationCodeRepository
{
    public async ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await DbContext.UserInfoVerificationCodes
           .Where(code => code.Id == codeId)
           .ExecuteUpdateAsync(setter => setter.SetProperty(code => code.IsActive, false), cancellationToken);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async  ValueTask<UserInfoVerificationCode> CreateAsync(UserInfoVerificationCode verificationCode, bool saveChanges, CancellationToken cancellationToken)
    {
        await DbContext.UserInfoVerificationCodes
          .Where(code => code.UserId == verificationCode.UserId && code.CodeType == verificationCode.CodeType)
          .ExecuteUpdateAsync(setter => setter.SetProperty(code => code.IsActive, false), cancellationToken);

        return await base.CreateAsync(verificationCode, saveChanges, cancellationToken);
    }

    public IQueryable<UserInfoVerificationCode> Get(Expression<Func<UserInfoVerificationCode, bool>>? predicate, bool asNoTracking)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<UserInfoVerificationCode?>GetByIdAsync(Guid codeId, bool asNoTracking, CancellationToken cancellationToken)
    {
        return base.GetByIdAsync(codeId, asNoTracking, cancellationToken);
    }
}
