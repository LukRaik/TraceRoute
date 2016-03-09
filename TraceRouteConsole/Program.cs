using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace TraceRouteConsole
{
    class Program
    {
        static void Konsola(object obj, EventArgs e)
        {
            Console.WriteLine((string)obj);
        }
        static void Main(string[] args)
        {



            WebClient client = new WebClient();
            Console.WriteLine(client.DownloadString("http://www.wp.pl"));

            
        }
    }
}
