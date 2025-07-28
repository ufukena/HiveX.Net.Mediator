namespace HiveX.Net.TestConsole.Abstractions.Abstracts.Entities
{

    public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>
    {
        
        public DateTime CreatedDate { get; set; }                       
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool DeletedFlg { get; set; }
        

    }

}
