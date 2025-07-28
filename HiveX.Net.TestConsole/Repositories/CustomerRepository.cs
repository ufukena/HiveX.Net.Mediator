using HiveX.Net.TestConsole.Abstractions.Abstracts.Repositories;
using HiveX.Net.TestConsole.Abstractions.Contracts.Context;
using HiveX.Net.TestConsole.Abstractions.Contracts.Repositories;
using HiveX.Net.TestConsole.Entites;
using HiveX.Net.TestConsole.Results;


namespace HiveX.Net.TestConsole.Repositories
{

    internal class CustomerRepository : BaseDbRepository, ICustomerRepository
    {
        public CustomerRepository(IDatabaseContext databaseContext) : base(databaseContext) { }


        public async Task Create(Customer entity)
        {
            //Here you would implement the logic to log the exception to your database.
        }

        public async Task Update(Customer entity)
        {
            //Here you would implement the logic to log the exception to your database.
        }

        public async Task Delete(int id)
        {
            //Here you would implement the logic to log the exception to your database.
        }

        public async Task<Customer?> Get(int id)
        {
            //Here you would implement the logic to log the exception to your database.
            return null; 
        }

        public async Task<List<CustomerResult>> GetAll()
        {
            //Here you would implement the logic to log the exception to your database.
            return new List<CustomerResult>();
        }


    }

}
