using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Entites;
using UserRole.Persistance.DataAccess;

namespace UserRole.Persistance.Repositoryies.UserRepository;
public class UserRepositorys : EntityBaseRepository<User, UserRoleDbContext>, IUserRepository
{

    public UserRepositorys(UserRoleDbContext dbContext) : base(dbContext)
    {

    }

    public ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking, CancellationToken cancellationToken = default)
    {
      return  base.GetByIdasync(id, asNoTracking, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, bool savechanges, CancellationToken cancellationToken = default)
    {
        return this.UpdateAsync(user, savechanges, cancellationToken);
    }

    public ValueTask<User> CreateAsync(User user, bool savechanges, CancellationToken cancellationToken)
    {
        return base.CreateAsync(user, savechanges, cancellationToken);
    }

    public ValueTask<User> DeleteAsync(User user, bool savechanges, CancellationToken cancellationToken)
    {
       return  base.DeleteAsync(user, savechanges, cancellationToken);
    }

    public IQueryable<User> Get(bool asNoTracking)
    {
        return base.Get(asNoTracking);
    }

    public async ValueTask<User> GetByEmail(string email)
    {
        return await DbContext.Users.FirstOrDefaultAsync(a => a.EmailAdress==email);
    }
}
