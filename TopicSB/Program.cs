using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace TopicSB
{
    class Program
    {
        private static ServiceBusConnection connection;
        static void Main(string[] args)
        {
            ServiceBusConnectionStringBuilder builder = new ServiceBusConnectionStringBuilder("Endpoint=sb://tuesdaysb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=4fDfjFmsgSyqw7y9802cPNv2hLabMxm263upNBxRw7k=");

            connection = new ServiceBusConnection(builder);
            ConnectToServiceBus().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private async static Task ConnectToServiceBus()
        {
            TopicClient client = new TopicClient(connection, "topicsb", RetryPolicy.Default);
            //Guid Productid = Guid.NewGuid();


           // var jsonContent = JsonConvert.SerializeObject(p1);

            Message msg = new Message(Encoding.ASCII.GetBytes("Test Message"));

            await client.SendAsync(msg);

        }
    }
}
