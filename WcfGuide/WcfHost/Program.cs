using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WcfGuide; 

namespace WcfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service1));

            host.Open();

            Console.WriteLine("Service is up and running..");
            Console.ReadLine();

            host.Close();
        }
    }
}
