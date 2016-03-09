using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Traceroute
{
    public partial class Form1 : Form
    {
        EventHandler EventTxLogAdd;
        TraceRoute Traceroute;
        List<string> lista_ip_string;
        List<string> kopia;
        bool doing_smthng;
        bool Tracerouted;
        int id = 0;
        public Form1()
        {
            InitializeComponent();
            EventTxLogAdd += tx_log;
            Traceroute = new TraceRoute();
            Traceroute.Loguj += EventTxLogAdd;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Tracerouted = false;
            doing_smthng = false;
        }

        public void tx_log(object o, EventArgs e)
        {
            if (list_logs.InvokeRequired == true)
            {
                list_logs.Invoke(EventTxLogAdd, new object[] { o, e });
            }
            else
            {
                //TracerouteEventArgs Args = (TracerouteEventArgs)e;
                if ((e as TracerouteEventArgs).zwrot == tx_msg.Text) (e as TracerouteEventArgs).zwrot = "true";
                list_logs.Items.Add(new ListViewItem((e as TracerouteEventArgs).ReturnArrayString(id)));
                kopia.Add((e as TracerouteEventArgs).adress + " " + (e as TracerouteEventArgs).roundtriptime + "ms " + "Ttl:" + (e as TracerouteEventArgs).Ttl);
                list_logs.Items[list_logs.Items.Count - 1].EnsureVisible();
                id++;
            }
        }
        private void bt_sledz_Click(object sender, EventArgs e)
        {
            if (doing_smthng == false)
            {
                list_logs.Items.Clear();
                Sledzenie.Dispose();
                Sledzenie.RunWorkerAsync();
                Sledzenie.WorkerSupportsCancellation = true;
            }
        }
        private void Sledzenie_DoWork(object sender, DoWorkEventArgs e)
        {
            doing_smthng = true;
            id = 1;
            kopia = new List<string>();
            Traceroute.SetNew();
            Traceroute.SetSend(tx_msg.Text);
            Traceroute.Sledz(tx_addres.Text,(int)numericUpDown1.Value,(int)numericUpDown2.Value,box_d.Checked);
            Tracerouted = true;
            MessageBox.Show("Koniec!");
            doing_smthng = false;
        }
        private void bt_log_Click(object sender, EventArgs e)
        {
            if (Tracerouted == true)
            {
                try
                {
                    Random generator = new Random();
                    string buffor = DateTime.Now.ToLongTimeString();
                    string[] arraybuffor = buffor.Split(':');
                    string name = "log_" +arraybuffor[0]+"_"+arraybuffor[1]+"_"+arraybuffor[2]+"_"+ DateTime.Now.ToShortDateString() + ".txt";
                    FileStream fs = new FileStream(name, FileMode.CreateNew);
                    StreamWriter stream = new StreamWriter(fs);
                    string buff = "";
                    int i = 0;
                    i = 50;
                    buff += "IP:";
                    i -= 3;
                    buff += new string(' ', i);
                    i = 10;
                    buff += "Czas:";
                    i -= 5;
                    buff += new string(' ', i);
                    buff += "Ttl:";
                    stream.WriteLine(buff);
                    foreach (string obj in kopia)
                    {
                        buff = "";
                        string[] buff2 = obj.Split(' ');
                        if (buff2.Length > 1)
                        {   
                            i = 0;
                            i = 50;
                            buff += buff2[0];
                            i -= buff2[0].Length;
                            buff += new string(' ', i);
                            i = 10;
                            buff += buff2[1];
                            i -= buff2[1].Length;
                            buff += new string(' ', i);
                            buff += buff2[2];

                        }
                        else
                        {
                            buff = buff2[0];
                        }
                        stream.WriteLine(buff);
                    }
                    stream.Close();
                    fs.Close();
                    MessageBox.Show("Utworzono:"+name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Plik o takiej nazwie juz istnieje");
                }
            }
            else
            {
                MessageBox.Show("Najpierw coś wyśledź");
            }

        }
        private void bt_infoo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By wyśledzić pakiet należy umieścić adres którego drogę chcemy przeanalizować i wpisać wiadomość od której bedzie zależyć wielkość buffora\nZ drogą-szuka po kolei danych punktów na drodze i przechodzi przez nie, na koniec dąży do punktu wyjściowego");
        }
        private void bt_stop_Click(object sender, EventArgs e)
        {
        }
        private void bt_routetrack_Click(object sender, EventArgs e)
        {
            if (doing_smthng == false)
            {
                list_logs.Items.Clear();
                SledzenieListowe.Dispose();
                SledzenieListowe.RunWorkerAsync();
                SledzenieListowe.WorkerSupportsCancellation = true;
            }
        }
        private void lista_ip_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void bt_add_listpref_Click(object sender, EventArgs e)
        {
            IPAddress adres = null;
            if (text_ip.Text != "" && IPAddress.TryParse(text_ip.Text, out adres)&&!lista_ip.Items.Contains(text_ip.Text))
            {
                lista_ip.Items.Add(text_ip.Text);
                text_ip.Text = "";
            }
            else
            {
                MessageBox.Show("Zly adres ip");
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void bt_del_list_Click(object sender, EventArgs e)
        {
            if (lista_ip.Items.Count > 0)
            {
                lista_ip.Items.RemoveAt(lista_ip.SelectedIndex);
            }


        }
        private void SledzenieListowe_DoWork(object sender, DoWorkEventArgs e)
        {
            doing_smthng = true;
            id = 1;
            lista_ip_string = new List<string>();
            foreach (string obj in lista_ip.Items)
            {
                lista_ip_string.Add(obj);
            }
            kopia = new List<string>();
            Traceroute.SetNew();
            Traceroute.SetSend(tx_msg.Text);
            Traceroute.Sledz_Lista(tx_addres.Text, (int)numericUpDown1.Value, (int)numericUpDown2.Value, box_d.Checked, lista_ip_string);
            Tracerouted = true;
            MessageBox.Show("Koniec!");
            doing_smthng = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Autor:Łukasz Zimnoch 2015");
        }
    }
}
