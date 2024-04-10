using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Shared.Infrastructure.Auth.Exceptions
{
    internal class PolicyNotFoundException : CryptLearnException
    {
        public string PolicyName { get; }

        public PolicyNotFoundException(string policyName) : base($"Authorization policy '{policyName}' was not found!")
        {
            PolicyName = policyName;
        }
    }
}
