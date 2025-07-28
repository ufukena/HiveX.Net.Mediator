

namespace HiveX.Net.TestConsole.Abstractions.Contracts.Generics
{
    public interface IRepositoryCommand<TEntity>
    {

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(int id);        

    }

}
