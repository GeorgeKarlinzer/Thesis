using CryptLearn.Shared.Abstractions.Auth;

namespace CryptLearn.Modules.ModuleManagement.Core;
internal class PermissionClaims : IPermissionClaimsProvider
{
    public const string ReadAccess = "module_management.can_read";
    public const string CreateAccess = "module_management.can_create";
    public const string UpdateAccess = "module_management.can_update";
    public const string DeleteAccess = "module_management.can_delete";

    public IEnumerable<string> GetClaims()
    {
        yield return ReadAccess;
        yield return CreateAccess;
        yield return UpdateAccess;
        yield return DeleteAccess;
    }
}
