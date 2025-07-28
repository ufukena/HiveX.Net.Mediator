using HiveX.Net.Mediator.Contracts.Markers;
using HiveX.Net.TestConsole.Models;
using HiveX.Net.TestConsole.Results;


namespace HiveX.Net.TestConsole.Queries
{
    

    public class CustomerGetQuery : IRequest<CustomerModel?>
    {
        public int Id { get; }
        public CustomerGetQuery(int id) => Id = id;

    }

    public class CustomerGetAllQuery : IRequest<List<CustomerResult>> { }
    

}
