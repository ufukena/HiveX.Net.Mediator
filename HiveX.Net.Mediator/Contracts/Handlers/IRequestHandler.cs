using HiveX.Net.Mediator.Contracts.Markers;


namespace HiveX.Net.Mediator.Contracts.Handlers
{
    /// <summary>
    /// Interface for a handler that processes a request of type TRequest.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request to be processed.</typeparam>
    /// <typeparam name="TResponse">The type of the result.</typeparam>
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Method that processes the request.
        /// </summary>
        /// <param name="request">The request to be processed.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The result of the operation.</returns>
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

}
