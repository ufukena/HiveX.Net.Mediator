using System.Data;


namespace HiveX.Net.TestConsole.Abstractions.Contracts.Context
{
    public interface IDatabaseContext
    {
        IDbConnection CreateSQLConnection() 
        {
            throw new NotImplementedException("This method has not been implemented. Please override it if needed.");
        }
        
    }
}
