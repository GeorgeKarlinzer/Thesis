using CryptLearn.Modules.AccessControl.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CryptLearn.Modules.AccessControl.Unit.Moqs
{
    internal class TestTokenFactory : IJwtTokenFactory
    {
        public Guid ExpectedUserId { get; set; }

        public JwtSecurityToken Create(string token)
        {
            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, ExpectedUserId.ToString()),
            };
            var jwtToken = new JwtSecurityToken(claims: jwtClaims);
            return jwtToken;
        }

        public JwtSecurityToken Create(string issuer = null, string audience = null, IEnumerable<Claim> claims = null, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null)
        {
            throw new NotImplementedException();
        }
    }
}
