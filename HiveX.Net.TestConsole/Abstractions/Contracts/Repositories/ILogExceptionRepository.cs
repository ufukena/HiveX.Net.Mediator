using HiveX.Net.TestConsole.Abstractions.Contracts.Markers;


namespace HiveX.Net.TestConsole.Abstractions.Contracts.Repositories
{
    public interface ILogExceptionRepository : IInjectable
    {
        Task Create<TRequest>(TRequest request, Exception exception);

    }

}
