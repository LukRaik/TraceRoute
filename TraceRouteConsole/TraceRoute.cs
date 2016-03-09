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
    class TraceRoute
    {
        /*
         * Traceroute example by Raik
         * 
         * 
         */
        string address;                 // Adres do pingowania
        public event EventHandler Loguj;       // Event logowania informacji
        Ping ping;                      // Klasa pingujaca
        PingOptions ping_options;       // Klasa opcji pingowania
        byte[] buffor;                  // Dane do wysłania
    
        public TraceRoute()
        {
            string wiadomosc = "BASEMSG";
            buffor = Encoding.ASCII.GetBytes(wiadomosc);
            ping = new Ping();
            ping_options = new PingOptions();
        }
        public void SetSend(string wiadomosc)
        {
            buffor = Encoding.ASCII.GetBytes(wiadomosc);
        }
        public void SetNew()
        {
            ping = new Ping();
            ping_options = new PingOptions();
        }
        public bool Sledz(string Adress)
        {
            try
            {
                ping_options.Ttl = 1;
                while (true)
                {
                        PingReply reply = ping.Send(Adress, 10000, buffor, ping_options);
                        if (reply.Status == IPStatus.Success)
                        {
                            Loguj(reply.Address.ToString()+" ms:"+reply.RoundtripTime.ToString()+" Zwrot:"+Encoding.ASCII.GetString(reply.Buffer), null);
                            return true;
                        }
                        else if (reply.Status == IPStatus.TtlExpired)
                        {
                            Loguj(reply.Address.ToString() + " ms:" + reply.RoundtripTime.ToString() + " Zwrot:" + Encoding.ASCII.GetString(reply.Buffer), null);
                            
                        }
                        else
                        {

                        }
                        ping_options.Ttl += 1;
                }
            }
            catch (Exception ex)
            {
                Loguj(ex.ToString(),null);
                return false;
            }
        }

    }
}
