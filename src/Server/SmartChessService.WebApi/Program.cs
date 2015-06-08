using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace SmartChessService.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Server running@ {0}", baseAddress);
                Console.WriteLine("Press return to terminate...");
                Console.ReadLine();
            }
        }
    }
}
