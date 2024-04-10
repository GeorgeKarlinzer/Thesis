using CryptLearn.Modules.ModuleManagement.Core.Entities;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;

namespace CryptLearn.Modules.ModuleManagement.Unit.Moqs
{
    internal class InMemoryUsersRepository : IUsersRepository
    {
        public List<User> Users { get; } = new();

        public Task<User> GetAsync(Guid id)
        {
            return Task.FromResult(Users.SingleOrDefault(x => x.Id == id));
        }
    }
}
