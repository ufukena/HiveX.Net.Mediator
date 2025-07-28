using HiveX.Net.Mediator.Contracts.Behaviors;
using HiveX.Net.Mediator.Contracts.Handlers;
using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.Mediator.Delegates;


namespace HiveX.Net.Mediator.Implementations
{

    public class PipelineExecutor<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IPipelineBehavior<TRequest, TResponse>> _behaviors;
        private readonly IRequestHandler<TRequest, TResponse> _handler;

        public PipelineExecutor( IEnumerable<IPipelineBehavior<TRequest, TResponse>> behaviors, IRequestHandler<TRequest, TResponse> handler)
        {
            _behaviors = behaviors ?? Enumerable.Empty<IPipelineBehavior<TRequest, TResponse>>();
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken)
        {
            RequestHandler<TResponse> handlerDelegate = () => _handler.Handle(request, cancellationToken);

            var pipeline = _behaviors.Reverse()
                           .Aggregate(handlerDelegate, (next, behavior) =>
                            () => behavior.Handle(request, cancellationToken, next));

            return pipeline();
        }
    }

}
