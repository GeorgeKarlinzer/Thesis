using MediatR;

namespace CryptLearn.Shared.Abstractions.Cqrs
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out T> : IRequest<T>
    {

    }
}
