using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Entites;

namespace UserRole.Persistance.Repositoryies.UserRepository;
public interface IUserRepository
{
    IQueryable<User> Get(bool asNoTracking);
    ValueTask<User> GetByEmail(string email);
    ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking, CancellationToken cancellationToken = default);
    ValueTask<User> CreateAsync(User user, bool savechanges, CancellationToken cancellationToken = default);
    ValueTask<User> UpdateAsync(User user, bool savechanges, CancellationToken cancellationToken = default);
    ValueTask<User> DeleteAsync(User user, bool savechanges, CancellationToken cancellationToken = default);
}

