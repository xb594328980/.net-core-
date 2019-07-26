using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OcelotConsole
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                for (var i = 0; i < 100; i++)
                {
                    Console.WriteLine(DateTime.Now);

                    var result = await client.GetAsync("http://localhost:4727/ocelot/ratelimit/5");
                    Console.WriteLine($"{result.StatusCode}, {result.Content.ReadAsStringAsync().Result}");
                    System.Threading.Thread.Sleep(1000);
                }
                Console.Read();
            }
        }
    }
}
