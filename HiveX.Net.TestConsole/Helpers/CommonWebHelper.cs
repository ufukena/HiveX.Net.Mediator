using HiveX.Net.TestConsole.Abstractions.Contracts.Helpers;
using System.Net;


namespace HiveX.Net.TestConsole.Helpers
{
    public class CommonWebHelper : ICommonWebHelper
    {



        public bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
      
    }
}
