using MediatR;

namespace CryptLearn.Shared.Abstractions.Cqrs
{
    public interface IQuery<out T> : IRequest<T>
    {

    }
}
