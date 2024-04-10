using Microsoft.AspNetCore.Authorization;

namespace CryptLearn.Shared.Abstractions.Auth
{
    public interface IPolicy
    {
        IEnumerable<IAuthorizationRequirement> Requirements { get; }
    }
}
