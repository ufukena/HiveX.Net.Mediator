

namespace HiveX.Net.TestConsole.Results
{
    // Represents the result of a customer (get all) queries, typically used to return data to the client.
    public class CustomerResult
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }

}
