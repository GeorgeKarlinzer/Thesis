using CryptLearn.Modules.AccessControl.Core.Entities;
using CryptLearn.Modules.AccessControl.Core.Repositories;
using CryptLearn.Shared.Abstractions.Time;

namespace CryptLearn.Modules.AccessControl.Unit.Moqs
{
    internal class InMemoryRefreshTokensRepository : IUserRefreshTokensRepository
    {
        private readonly IClock _clock;

        public List<UserRefreshToken> UserRefreshTokens { get; } = new();

        public InMemoryRefreshTokensRepository(IClock clock)
        {
            _clock = clock;
        }

        public Task AddAsync(UserRefreshToken token)
        {
            UserRefreshTokens.Add(token);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(UserRefreshToken token)
        {
            UserRefreshTokens.Remove(token);
            return Task.CompletedTask;
        }

        public Task DeleteExpiredTokensAsync(Guid userId)
        {
            var tokens = UserRefreshTokens.Where(x => x.UserId == userId && _clock.CurrentDate() > x.ValidTo);
            foreach (var token in tokens)
            {
                UserRefreshTokens.Remove(token);
            }
            return Task.CompletedTask;
        }

        public Task DeleteUserTokensAsync(Guid userId)
        {
            var tokens = UserRefreshTokens.Where(x => x.UserId == userId).ToList();
            foreach (var token in tokens)
            {
                UserRefreshTokens.Remove(token);
            }
            return Task.CompletedTask;
        }

        public Task<UserRefreshToken> GetAsync(string token)
        {
            var userToken = UserRefreshTokens.FirstOrDefault(x => x.Value == token);
            return Task.FromResult(userToken);
        }

        public Task UpdateAsync(UserRefreshToken token)
        {
            return Task.CompletedTask;
        }
    }
}
