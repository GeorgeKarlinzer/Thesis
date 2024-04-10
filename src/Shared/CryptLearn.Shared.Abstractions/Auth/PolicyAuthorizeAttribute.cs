using Microsoft.AspNetCore.Authorization;

namespace CryptLearn.Shared.Abstractions.Auth
{
    public class AuthorizeByPolicyAttribute<TPolicy> : AuthorizeAttribute 
        where TPolicy : IPolicy
    {
        public AuthorizeByPolicyAttribute()
        {
            Policy = typeof(TPolicy).FullName;
        }
    }
}
