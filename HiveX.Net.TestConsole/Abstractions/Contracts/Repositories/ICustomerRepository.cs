using HiveX.Net.TestConsole.Abstractions.Contracts.Generics;
using HiveX.Net.TestConsole.Abstractions.Contracts.Markers;
using HiveX.Net.TestConsole.Entites;
using HiveX.Net.TestConsole.Results;


namespace HiveX.Net.TestConsole.Abstractions.Contracts.Repositories
{
    public interface ICustomerRepository : IInjectable, IRepositoryCommand<Customer>, IRepositoryQuery<Customer, CustomerResult>
    {
        
    }

}
