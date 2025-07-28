using HiveX.Net.Mediator.Contracts.Behaviors;
using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.Mediator.Delegates;
using HiveX.Net.TestConsole.Abstractions.Contracts.Repositories;


namespace HiveX.Net.TestConsole.Behaviors
{
    public class LogExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogExceptionRepository _repositorysitory;

        public LogExceptionBehavior(ILogExceptionRepository logExceptionRepository)
        {
            _repositorysitory = logExceptionRepository;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandler<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                await _repositorysitory.Create(request, ex);
                throw;
            }
        }
    }

}
