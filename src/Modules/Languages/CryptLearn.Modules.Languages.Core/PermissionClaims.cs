using CryptLearn.Shared.Abstractions.Auth;

namespace CryptLearn.Modules.Languages.Core;
public class PermissionClaims : IPermissionClaimsProvider
{
    public const string ReadAccess = "languages.can_read";
    public const string CreateAccess = "languages.can_create";
    public const string UpdateAccess = "languages.can_update";

    public IEnumerable<string> GetClaims()
    {
        yield return ReadAccess;
        yield return CreateAccess;
        yield return UpdateAccess;
    }
}
