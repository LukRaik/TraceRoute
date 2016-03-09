using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Traceroute
{
    
    class TracerouteEventArgs : EventArgs
    {
        /*
     * Klasa z argumentami do delegata logującego informacje z TraceRoute
     * 
     * 
     */
        public string adress { get; private set; }
        public string roundtriptime { get; private set; }
        public string zwrot;
        public int Ttl { get; private set; }
        public TracerouteEventArgs(string Adres, string RTT, string Zwrot, int Ttl)
        {
            adress = Adres;
            roundtriptime = RTT;
            zwrot = Zwrot;
            this.Ttl = Ttl;
        }
        public string[] ReturnArrayString(int id)
        {
            string[] buffor = new string[5];
            buffor[0] = Convert.ToString(id);
            buffor[1] = adress;
            if (zwrot == "") buffor[2] = "*";
            else buffor[2] = zwrot;
            buffor[3] = roundtriptime;
            buffor[4] = Convert.ToString(Ttl);
            return buffor;
        }
    }
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
        int licznik = 0;                // Licznik pingow bez odpowiedzi
        int curr_id = 0;                // Aktualny numer trasy
        const int max_licznik = 20;
        public List<string> Lista_adresow { get; private set; }
        public TraceRoute()
        {
            string wiadomosc = "BASEMSG";                   // Tworzenie buffora w razie braku przypisania
            buffor = Encoding.ASCII.GetBytes(wiadomosc);    //
            ping = new Ping();                              // Obiekt pingujacy
            ping_options = new PingOptions();               // Obiekt argumentow obiektu pingujacego
        }
        public void SetSend(string wiadomosc)
        {
            buffor = Encoding.ASCII.GetBytes(wiadomosc);        // Tworzenie nowego buffora
        }
        public void SetNew()                // Tworzy nowe obiekty ping,pingoptions w razie chęci ponownego śledzenia
        {
            ping = new Ping();
            ping_options = new PingOptions();
        }
        public bool Sledz(string Adress,int ms,int max_track,bool aliases)
        {
            Lista_adresow = new List<string>();         // Lista z wszystkimi adresami;
            licznik = 0;                                // Licznik braku odpowiedzi
            curr_id = 1;                                // Licznik id
            try
            {
                ping_options.Ttl = 1;                   // Ustawiamy początkowo Time to live na 1
                while (max_track==0||(max_track>0&&curr_id<=max_track))    // Jesli nie ma limitu albo jest i nie zostal osiagniety
                {
                        PingReply reply = ping.Send(Adress, ms, buffor, ping_options);  // Wysylamy icmp
                        string buff = "";                                               // Przygotowywujemy wyjscie
                        if (reply.Status == IPStatus.Success)                           // Ping udany(tzn dotarł do swgo celu)
                        {
                            
                            try
                            {
                                if (!aliases) // Jesli chcemy aliasy
                                {
                                    IPHostEntry host;                               // Informacje o hoscie
                                    host = Dns.GetHostEntry(reply.Address);         // Pobiernie informacji o hoscie na podstawie ip z pingu
                                    buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                                }
                            }
                            catch (Exception ex)
                            {
                                //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                            }
                            finally
                            {
                                Lista_adresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                                //Loguje wpis
                                Loguj(null, new TracerouteEventArgs(buff+ reply.Address.ToString(), reply.RoundtripTime.ToString(), Encoding.ASCII.GetString(reply.Buffer), ping_options.Ttl));
                            }
                            return true;        //Zwraca true, koniec funkcji
                        }
                        else if (reply.Status == IPStatus.TtlExpired||reply.Status != IPStatus.TimedOut)    // Jesli zwrocono informacje o skonczeniu ttl lub nie dostano odpowiedzi w okreslonym czasie
                        {
                            licznik = 0;    // Zerujemy licznik nieudanych pingow
                            if (reply.Address != null)
                            {
                                try
                                {
                                        reply = ping.Send(reply.Address, ms, buffor); // Pingujemy zwrócony adres by dostać czas potrzebny na połączenie z nim
                                        if (!aliases) // Jesli chcemy aliasy
                                        {
                                            IPHostEntry host;                               // Informacje o hoscie
                                            host = Dns.GetHostEntry(reply.Address);         // Pobiernie informacji o hoscie na podstawie ip z pingu
                                            buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                                        }
                                }
                                catch (Exception ex)
                                {
                                    //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                                }
                                finally
                                {
                                    curr_id++;                                   // Kolejny numer w liscie
                                    Lista_adresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                                    //Loguje wpis
                                    Loguj(null, new TracerouteEventArgs(buff + reply.Address.ToString(), reply.RoundtripTime.ToString(), Encoding.ASCII.GetString(reply.Buffer), ping_options.Ttl));
                                }
                            }
                        }
                        else
                        {
                            curr_id++;                      // Naliczanie numeru drogi
                            if (licznik == max_licznik)     // Licznik braku odpowiedzi równy maksymalnej ilości braku odpowiedzi
                            {
                                // Informujemy o błędzie
                                Loguj(null, new TracerouteEventArgs("Prawdopodobnie napotkano firewalla", "*", "*", ping_options.Ttl));
                                return false;
                            }
                            Lista_adresow.Add("*");
                            licznik++;
                            Loguj(null, new TracerouteEventArgs("*", "*", "*", ping_options.Ttl));
                        }
                        ping_options.Ttl += 1;              // Zwiększamy żywotność
                }
            }
            catch (Exception ex)
            {
                // W razie błędu wyświetlamy treść błędu
                System.Windows.Forms.MessageBox.Show(ex.ToString());
     
                return false;
            }
            return false;
        }
        public bool Sledz_Lista(string Adress,int ms,int max_track,bool aliases,List<string> adresy)
        {
            Lista_adresow = new List<string>();         // Lista z wszystkimi adresami;
            licznik = 0;                                // Licznik braku odpowiedzi
            curr_id = 1;                                // Licznik id
            int przeskok = 1;                           // Wartosć o która przeskakujemy ttl
            try
            {
                ping_options.Ttl = 1;                   // Ustawiamy początkowo Time to live na 1
                while (max_track == 0 || (max_track > 0 && ping_options.Ttl <= max_track))  // Jesli nie ma limitu albo jest i nie zostal osiagniety
                {
                    PingReply reply = ping.Send(Adress, ms, buffor, ping_options);      // Pingujemy
                    string buff = "";                           // Przygotowanie wyjścia
                    if (reply.Status == IPStatus.Success)       // Udane, osiągnięto cel
                    {
                        
                        try
                        {
                            #region Sprawdzanie
                            if (adresy.Count > 0)               // Jeśli jest jesze jakiś żądany punkt
                            {
                                if (adresy[0] == reply.Address.ToString())  // Aktualne miejsce jest tym punktem
                                {
                                    if (adresy.Count == 1)                  // Zostal ostatni punkt
                                    {
                                            przeskok = 1;                   // Skaczemy o 1
                                            adresy.RemoveAt(0);             // Usuwamy punkt z listy
                                    }
                                    else if (adresy.Count > 0)              // Jest więcej niż jeden punkt
                                    {
                                        if (Lista_adresow.Contains(adresy[1]))  // Wystąpił już adres który będzie celem w przyszłości
                                        {
                                            przeskok *= -1;                     // Idziemy z ttl w dół
                                            adresy.RemoveAt(0);                 // Usuwamy punkt
                                        }
                                        else                                    // W innym przypadku
                                        {
                                            przeskok = 1;                       // Skaczemy do przodu
                                            adresy.RemoveAt(0);                 // Usuwamy punkt z listy
                                        }
                                    }
                                    else if (przeskok < 0)                      // Gdy skaczemy w dół i nic nie wiadomo, idziemy w góre
                                    {
                                        przeskok = 1;
                                    }

                                }
                            }
                            #endregion
                            if (!aliases) // Jesli chcemy aliasy
                            {
                                IPHostEntry host;                               // Informacje o hoscie
                                host = Dns.GetHostEntry(reply.Address);         // Pobiernie informacji o hoscie na podstawie ip z pingu
                                buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                            }
                        }
                        catch (Exception ex)
                        {
                            //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                        }
                        finally
                        {
                            Lista_adresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                            //Loguje wpis
                            Loguj(null, new TracerouteEventArgs(buff + reply.Address.ToString(), reply.RoundtripTime.ToString(), Encoding.ASCII.GetString(reply.Buffer), ping_options.Ttl));
                        }
                        if(adresy.Count==0) return true;        // Jesli nie ma już żądanych punktów, zakończ funkcje
                    }
                    else if (reply.Status == IPStatus.TtlExpired || reply.Status != IPStatus.TimedOut)  // Jesli zwrocono informacje o skonczeniu ttl lub nie dostano odpowiedzi w okreslonym czasie
                    {
                        curr_id++;      // Aktualny numer drogi
                        licznik = 0;    // Zerujemy licznik nieudanych pingow
                        if (reply.Address != null)
                        {
                            try
                            {
                                #region Sprawdzanie
                                if (adresy.Count > 0)               // Jeśli jest jesze jakiś żądany punkt
                                {
                                    if (adresy[0] == reply.Address.ToString())  // Aktualne miejsce jest tym punktem
                                    {
                                        if (adresy.Count == 1)                  // Zostal ostatni punkt
                                        {
                                            przeskok = 1;                   // Skaczemy o 1
                                            adresy.RemoveAt(0);             // Usuwamy punkt z listy
                                        }
                                        else if (adresy.Count > 0)              // Jest więcej niż jeden punkt
                                        {
                                            if (Lista_adresow.Contains(adresy[1]))  // Wystąpił już adres który będzie celem w przyszłości
                                            {
                                                przeskok *= -1;                     // Idziemy z ttl w dół
                                                adresy.RemoveAt(0);                 // Usuwamy punkt
                                            }
                                            else                                    // W innym przypadku
                                            {
                                                przeskok = 1;                       // Skaczemy do przodu
                                                adresy.RemoveAt(0);                 // Usuwamy punkt z listy
                                            }
                                        }
                                        else if (przeskok < 0)                      // Gdy skaczemy w dół i nic nie wiadomo, idziemy w góre
                                        {
                                            przeskok = 1;
                                        }

                                    }
                                }
                                #endregion
                                reply = ping.Send(reply.Address, ms, buffor); // Pingujemy zwrócony adres by dostać czas potrzebny na połączenie z nim
                                if (!aliases) // Jesli chcemy aliasy
                                {
                                    IPHostEntry host;                               // Informacje o hoscie
                                    host = Dns.GetHostEntry(reply.Address);         // Pobiernie informacji o hoscie na podstawie ip z pingu
                                    buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                                }
                            }
                            catch (Exception ex)
                            {
                                //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                            }
                            finally
                            {
                                Lista_adresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                                //Loguje wpis
                                Loguj(null, new TracerouteEventArgs(buff + reply.Address.ToString(), reply.RoundtripTime.ToString(), Encoding.ASCII.GetString(reply.Buffer), ping_options.Ttl));
                            }
                        }
                    }
                    else
                    {
                        curr_id++;                      // Naliczanie numeru drogi
                        if (licznik == max_licznik)     // Licznik braku odpowiedzi równy maksymalnej ilości braku odpowiedzi
                        {
                            // Informujemy o błędzie
                            Loguj(null, new TracerouteEventArgs("Prawdopodobnie napotkano firewalla", "*", "*", ping_options.Ttl));
                            return false;
                        }
                        Lista_adresow.Add("*");
                        licznik++;
                        Loguj(null, new TracerouteEventArgs("*", "*", "*", ping_options.Ttl));
                    }
                    ping_options.Ttl += przeskok;       // Zmieniamyy żywotność
                    if (przeskok == -1)                 // Jesli schodzimy w dół usuwaj adresy które są powyżej
                    {
                        while (Lista_adresow.Contains(reply.Address.ToString()))
                        {
                            Lista_adresow.Remove(reply.Address.ToString());
                        }
                    }
                    if(curr_id>=max_track&&max_track>0)              // Jeśli aktualna droga jest większa/równa maksymalnej, zakończ z powodzeniem
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

                return false;
            }
            return false;
        }
    }


}
