using CryptLearn.Shared.Abstractions.Auth;

namespace CryptLearn.Modules.ModuleSolving.Core;
internal class PermissionClaims : IPermissionClaimsProvider
{
    public const string Access = "module_solving.can_solve";

    public IEnumerable<string> GetClaims()
    {
        yield return Access;
    }
}
