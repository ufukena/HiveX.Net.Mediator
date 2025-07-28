using HiveX.Net.TestConsole.Abstractions.Abstracts.Repositories;
using HiveX.Net.TestConsole.Abstractions.Contracts.Context;
using HiveX.Net.TestConsole.Abstractions.Contracts.Repositories;


namespace HiveX.Net.TestConsole.Repositories
{
    public class LogExceptionRepository : BaseDbRepository, ILogExceptionRepository
    {

        public LogExceptionRepository(IDatabaseContext databaseContext) : base(databaseContext) { }


        public async Task Create<TRequest>(TRequest request, Exception exception)
        {
            //Here you would implement the logic to log the exception to your database.
        }
        
    }
}
