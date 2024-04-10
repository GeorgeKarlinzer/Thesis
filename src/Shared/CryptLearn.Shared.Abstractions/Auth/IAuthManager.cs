using System.Security.Claims;

namespace CryptLearn.Shared.Abstractions.Auth
{
    public interface IAuthManager
    {
        string CreateToken(Guid userId, DateTime expireDate, IEnumerable<Claim> claims = null);
    }
}