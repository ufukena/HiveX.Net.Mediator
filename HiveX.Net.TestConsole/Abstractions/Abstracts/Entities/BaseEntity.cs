using HiveX.Net.TestConsole.Abstractions.Contracts.Markers;


namespace HiveX.Net.TestConsole.Abstractions.Abstracts.Entities
{

    public abstract class BaseEntity<TId> : IBaseEntity
    {
        
        //Guid or Int
        
        public virtual TId Id { get; set; } = default!;

    }

}
