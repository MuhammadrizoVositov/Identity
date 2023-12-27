using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Application.Services;
using UserRole.Domain.Entites;
using UserRole.Persistance.Repositoryies.UserRepository;

namespace UserRole.Infrostructure;
public class UserService:IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async ValueTask<User> CreateAsync(User user, bool savechanges, CancellationToken cancellationToken = default)
    {
        return await _userRepository.CreateAsync(user, savechanges, cancellationToken);
    }

    public async ValueTask<User> DeleteAsync(User user, bool savechanges, CancellationToken cancellationToken = default)
    {
        var delete = _userRepository.GetByIdAsync(user.Id,savechanges,cancellationToken);
        return await _userRepository.DeleteAsync(user, savechanges, cancellationToken);
    }

    public IQueryable<User> Get(bool asNoTracking)
    {
        return _userRepository.Get(asNoTracking).AsQueryable();
    }

    public  ValueTask<User> GetByEmail(string email)
    {
        return _userRepository.GetByEmail(email);
    }

    public async ValueTask<User> GetByIdAsync(Guid id, bool asNoTracking, CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public async ValueTask<User> UpdateAsync(User user, bool savechanges, CancellationToken cancellationToken = default)
    {
        var update =await _userRepository.GetByIdAsync(user.Id, savechanges, cancellationToken);
        update.FirstName = user.FirstName;
        update.LastName = user.LastName;
        update.EmailAdress = user.EmailAdress;

        return await _userRepository.UpdateAsync(user, savechanges, cancellationToken);
    }
}
