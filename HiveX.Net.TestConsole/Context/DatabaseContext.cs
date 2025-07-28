using System.Data;
using HiveX.Net.TestConsole.Abstractions.Contracts.Context;
using Microsoft.Data.SqlClient;


namespace HiveX.Net.TestConsole.Context
{
    public class DatabaseContext : IDatabaseContext
    {

        public string GetConnection => "Your connection goes gere..";


        //////////////////////////////////////////////////////////////////////////////////////////////////////

        // IDatabaseContext interface implementation
        public IDbConnection CreateSQLConnection() => new SqlConnection(GetConnection);
      
    }

}
