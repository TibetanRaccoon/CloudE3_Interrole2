using System;
using System.Threading;

namespace Client_App
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a string");
                var input = Console.ReadLine();
                var proxy = Connection.GetProxy();

                if(proxy==null)
                {
                    Console.WriteLine("Failed to create proxy");
                    Thread.Sleep(2000);
                    continue;
                }

                proxy.DoSomethig(input);
            }
        }
    }
}
