using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.Mediator.Delegates;


namespace HiveX.Net.Mediator.Contracts.Behaviors
{
    public interface IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle( TRequest request, CancellationToken cancellationToken, RequestHandler<TResponse> next);

    }
}
