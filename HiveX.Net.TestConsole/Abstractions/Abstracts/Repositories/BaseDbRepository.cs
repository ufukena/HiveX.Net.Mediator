using HiveX.Net.TestConsole.Abstractions.Contracts.Context;


namespace HiveX.Net.TestConsole.Abstractions.Abstracts.Repositories
{

    public abstract class BaseDbRepository 
    {
        
        public string Sql { get; set; } = string.Empty;

        public readonly IDatabaseContext DatabaseContext;


        public BaseDbRepository(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }


    }



}
