

namespace HiveX.Net.TestConsole.Abstractions.Contracts.Generics
{
    public interface IRepositoryQuery<TEntity, TEntityResult>
    {

        Task<TEntity?> Get(int id)
        {
            throw new NotImplementedException("This method has not been implemented. Please override it if needed.");
        }

        Task<List<TEntityResult>> GetAll()
        {
            throw new NotImplementedException("This method has not been implemented. Please override it if needed.");
        }

        Task<IEnumerable<TEntityResult>> GetAllAsEnumerable()
        {
            throw new NotImplementedException("This method has not been implemented. Please override it if needed.");
        }

    }



}
