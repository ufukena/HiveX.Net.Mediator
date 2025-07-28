using HiveX.Net.TestConsole.Abstractions.Contracts.Markers;


namespace HiveX.Net.TestConsole.Abstractions.Abstracts.Entities
{
    public abstract class BaseLogException : IBaseEntity
    {        
        public DateTime CreatedDate { get; set; }        
        public string ErrorMessage { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        
    }
}
