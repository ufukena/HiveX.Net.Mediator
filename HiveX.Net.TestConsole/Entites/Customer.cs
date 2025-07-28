using HiveX.Net.TestConsole.Abstractions.Abstracts.Entities;


namespace HiveX.Net.TestConsole.Entites
{
    public class Customer : BaseAuditableEntity<int>
    {        
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }

}
