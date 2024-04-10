using MediatR;

namespace CryptLearn.Shared.Abstractions.Cqrs
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {

    }
}
