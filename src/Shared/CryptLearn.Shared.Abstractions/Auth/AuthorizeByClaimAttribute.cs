using Microsoft.AspNetCore.Authorization;

namespace CryptLearn.Shared.Abstractions.Auth
{
    public class AuthorizeByClaimAttribute : AuthorizeAttribute
    {
        public AuthorizeByClaimAttribute(string permissionClaim) : base(permissionClaim) { }
    }
}
