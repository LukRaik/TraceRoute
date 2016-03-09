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
        public string Adress { get; private set; }
        public string Roundtriptime { get; private set; }
        public string Zwrot;
        public int Ttl { get; private set; }
        public TracerouteEventArgs(string adres, string rtt, string zwrot, int ttl)
        {
            Adress = adres;
            Roundtriptime = rtt;
            this.Zwrot = zwrot;
            this.Ttl = ttl;
        }
        public string[] ReturnArrayString(int id)
        {
            string[] buffor = new string[5];
            buffor[0] = Convert.ToString(id);
            buffor[1] = Adress;
            if (Zwrot == "") buffor[2] = "*";
            else buffor[2] = Zwrot;
            buffor[3] = Roundtriptime;
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
        public event EventHandler Loguj;       // Event logowania informacji
        Ping _ping;                      // Klasa pingujaca
        PingOptions _pingOptions;       // Klasa opcji pingowania
        byte[] _buffor;                  // Dane do wysłania
        int _licznik = 0;                // Licznik pingow bez odpowiedzi
        int _currId = 0;                // Aktualny numer trasy
        const int MaxLicznik = 20;
        public List<string> ListaAdresow { get; private set; }
        public TraceRoute()
        {
            string wiadomosc = "BASEMSG";                   // Tworzenie buffora w razie braku przypisania
            _buffor = Encoding.ASCII.GetBytes(wiadomosc);    //
            _ping = new Ping();                              // Obiekt pingujacy
            _pingOptions = new PingOptions();               // Obiekt argumentow obiektu pingujacego
        }
        public void SetSend(string wiadomosc)
        {
            _buffor = Encoding.ASCII.GetBytes(wiadomosc);        // Tworzenie nowego buffora
        }
        public void SetNew()                // Tworzy nowe obiekty ping,pingoptions w razie chęci ponownego śledzenia
        {
            _ping = new Ping();
            _pingOptions = new PingOptions();
        }
        public bool Sledz(string adress, int ms, int maxTrack, bool aliases)
        {
            ListaAdresow = new List<string>();         // Lista z wszystkimi adresami;
            _licznik = 0;                                // Licznik braku odpowiedzi
            _currId = 1;                                // Licznik id
            try
            {
                _pingOptions.Ttl = 1;                   // Ustawiamy początkowo Time to live na 1
                while (maxTrack == 0 || (maxTrack > 0 && _currId <= maxTrack))    // Jesli nie ma limitu albo jest i nie zostal osiagniety
                {
                    var timeStart = DateTime.Now.Millisecond;
                    PingReply reply = _ping.Send(adress, ms, _buffor, _pingOptions);  // Wysylamy icmp
                    var timeEnd = DateTime.Now.Millisecond;
                    string buff = "";                                               // Przygotowywujemy wyjscie
                    if (reply != null && reply.Status == IPStatus.Success)                           // Ping udany(tzn dotarł do swgo celu)
                    {

                        try
                        {
                            if (!aliases) // Jesli chcemy aliasy
                            {
                                var host = Dns.GetHostEntry(reply.Address);
                                buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                            }
                        }
                        catch (Exception ex)
                        {
                            //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                        }
                        finally
                        {
                            ListaAdresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                                                                         //Loguje wpis
                            Loguj?.Invoke(null, new TracerouteEventArgs(buff + reply.Address.ToString(), reply.RoundtripTime.ToString(), Encoding.ASCII.GetString(reply.Buffer), _pingOptions.Ttl));
                        }
                        return true;        //Zwraca true, koniec funkcji
                    }
                    else if (reply != null && (reply.Status == IPStatus.TtlExpired || reply.Status != IPStatus.TimedOut))    // Jesli zwrocono informacje o skonczeniu ttl lub nie dostano odpowiedzi w okreslonym czasie
                    {
                        _licznik = 0;    // Zerujemy licznik nieudanych pingow
                        if (reply.Address != null)
                        {
                            try
                            {
                                if (!aliases) // Jesli chcemy aliasy
                                {
                                    var host = Dns.GetHostEntry(reply.Address);
                                    buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                                }
                            }
                            catch (Exception ex)
                            {
                                //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                            }
                            finally
                            {
                                _currId++;                                   // Kolejny numer w liscie
                                ListaAdresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                                                                             //Loguje wpis
                                Loguj?.Invoke(null, new TracerouteEventArgs(buff + reply.Address.ToString(), (timeEnd-timeStart).ToString(), Encoding.ASCII.GetString(reply.Buffer), _pingOptions.Ttl));
                            }
                        }
                    }
                    else
                    {
                        _currId++;                      // Naliczanie numeru drogi
                        if (_licznik == MaxLicznik)     // Licznik braku odpowiedzi równy maksymalnej ilości braku odpowiedzi
                        {
                            // Informujemy o błędzie
                            Loguj?.Invoke(null, new TracerouteEventArgs("Prawdopodobnie napotkano firewalla", "*", "*", _pingOptions.Ttl));
                            return false;
                        }
                        ListaAdresow.Add("*");
                        _licznik++;
                        Loguj?.Invoke(null, new TracerouteEventArgs("*", "*", "*", _pingOptions.Ttl));
                    }
                    _pingOptions.Ttl += 1;              // Zwiększamy żywotność
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
        public bool Sledz_Lista(string adress, int ms, int maxTrack, bool aliases, List<string> adresy)
        {
            ListaAdresow = new List<string>();         // Lista z wszystkimi adresami;
            _licznik = 0;                                // Licznik braku odpowiedzi
            _currId = 1;                                // Licznik id
            int przeskok = 1;                           // Wartosć o która przeskakujemy ttl
            try
            {
                _pingOptions.Ttl = 1;                   // Ustawiamy początkowo Time to live na 1
                while (maxTrack == 0 || (maxTrack > 0 && _pingOptions.Ttl <= maxTrack))  // Jesli nie ma limitu albo jest i nie zostal osiagniety
                {
                    PingReply reply = _ping.Send(adress, ms, _buffor, _pingOptions);      // Pingujemy
                    string buff = "";                           // Przygotowanie wyjścia
                    if (reply != null && reply.Status == IPStatus.Success)       // Udane, osiągnięto cel
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
                                        if (ListaAdresow.Contains(adresy[1]))  // Wystąpił już adres który będzie celem w przyszłości
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
                                var host = Dns.GetHostEntry(reply.Address);
                                buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                            }
                        }
                        catch (Exception ex)
                        {
                            //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                        }
                        finally
                        {
                            ListaAdresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                            //Loguje wpis
                            if (Loguj != null)
                                Loguj(null, new TracerouteEventArgs(buff + reply.Address.ToString(), reply.RoundtripTime.ToString(), Encoding.ASCII.GetString(reply.Buffer), _pingOptions.Ttl));
                        }
                        if (adresy.Count == 0) return true;        // Jesli nie ma już żądanych punktów, zakończ funkcje
                    }
                    else if (reply != null && (reply.Status == IPStatus.TtlExpired || reply.Status != IPStatus.TimedOut))  // Jesli zwrocono informacje o skonczeniu ttl lub nie dostano odpowiedzi w okreslonym czasie
                    {
                        _currId++;      // Aktualny numer drogi
                        _licznik = 0;    // Zerujemy licznik nieudanych pingow
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
                                            if (ListaAdresow.Contains(adresy[1]))  // Wystąpił już adres który będzie celem w przyszłości
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
                                reply = _ping.Send(reply.Address, ms, _buffor); // Pingujemy zwrócony adres by dostać czas potrzebny na połączenie z nim
                                if (!aliases) // Jesli chcemy aliasy
                                {
                                    var host = Dns.GetHostEntry(reply.Address);
                                    buff = "(" + host.HostName + ")";               // Dodawanie do buffora wyjscia
                                }
                            }
                            catch (Exception ex)
                            {
                                //W razie nieudanego pobrania aliasu nic się nie stanie i program poleci dalej
                            }
                            finally
                            {
                                if (reply != null)
                                {
                                    ListaAdresow.Add(reply.Address.ToString()); // Dopisuje adres do listy
                                    //Loguje wpis
                                    if (Loguj != null)
                                        Loguj(null, new TracerouteEventArgs(buff + reply.Address.ToString(), reply.RoundtripTime.ToString(), Encoding.ASCII.GetString(reply.Buffer), _pingOptions.Ttl));
                                }
                            }
                        }
                    }
                    else
                    {
                        _currId++;                      // Naliczanie numeru drogi
                        if (_licznik == MaxLicznik)     // Licznik braku odpowiedzi równy maksymalnej ilości braku odpowiedzi
                        {
                            // Informujemy o błędzie
                            Loguj(null, new TracerouteEventArgs("Prawdopodobnie napotkano firewalla", "*", "*", _pingOptions.Ttl));
                            return false;
                        }
                        ListaAdresow.Add("*");
                        _licznik++;
                        Loguj(null, new TracerouteEventArgs("*", "*", "*", _pingOptions.Ttl));
                    }
                    _pingOptions.Ttl += przeskok;       // Zmieniamyy żywotność
                    if (przeskok == -1)                 // Jesli schodzimy w dół usuwaj adresy które są powyżej
                    {
                        while (ListaAdresow.Contains(reply.Address.ToString()))
                        {
                            ListaAdresow.Remove(reply.Address.ToString());
                        }
                    }
                    if (_currId >= maxTrack && maxTrack > 0)              // Jeśli aktualna droga jest większa/równa maksymalnej, zakończ z powodzeniem
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
