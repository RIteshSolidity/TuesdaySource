using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace Subscriber2
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
            SubscriptionClient client = new SubscriptionClient(connection, "topicsb", "topicsSubscription1", ReceiveMode.PeekLock, RetryPolicy.Default);
            //Guid Productid = Guid.NewGuid();

            client.RegisterMessageHandler(happypath, unhappypath);
            // var jsonContent = JsonConvert.SerializeObject(p1);
            await Task.CompletedTask;

        }

        private static Task unhappypath(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }

        private async static Task happypath(Message arg1, CancellationToken arg2)
        {
            string incomingMessage = Encoding.ASCII.GetString(arg1.Body);

            Console.WriteLine(incomingMessage);
            await Task.CompletedTask;
        }
    }
}
