using CryptLearn.Modules.AccessControl.Core.Entities;
using CryptLearn.Modules.AccessControl.Core.Repositories;

namespace CryptLearn.Modules.AccessControl.Unit.Moqs
{
    internal class InMemoryUsersRepository : IUsersRepository
    {
        public readonly List<User> users = new();

        public Task<User> GetAsync(Guid id)
        {
            return Task.FromResult(users.Single(x => x.Id == id));
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return Task.FromResult(users.FirstOrDefault(x => x.NormalizedEmail == email.ToLowerInvariant()));
        }

        public Task<User> GetByUserNameAsync(string userName)
        {
            return Task.FromResult(users.FirstOrDefault(x => x.NormalizedUserName == userName.ToLowerInvariant()));
        }

        public Task AddAsync(User user)
        {
            users.Add(user);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(User user)
        {
            return Task.CompletedTask;
        }
    }
}
