using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Shared.Infrastructure.Exceptions
{
    internal interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}
