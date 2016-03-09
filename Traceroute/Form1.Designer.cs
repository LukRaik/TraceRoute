namespace Traceroute
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda wsparcia projektanta - nie należy modyfikować
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.list_logs = new System.Windows.Forms.ListView();
            this.col_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_ms = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_zwrot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_ttl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tx_addres = new System.Windows.Forms.TextBox();
            this.bt_sledz = new System.Windows.Forms.Button();
            this.Adres = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tx_msg = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.Sledzenie = new System.ComponentModel.BackgroundWorker();
            this.bt_log = new System.Windows.Forms.Button();
            this.bt_infoo = new System.Windows.Forms.Button();
            this.box_d = new System.Windows.Forms.CheckBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bt_routetrack = new System.Windows.Forms.Button();
            this.lista_ip = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.text_ip = new System.Windows.Forms.TextBox();
            this.bt_add_listpref = new System.Windows.Forms.Button();
            this.bt_del_list = new System.Windows.Forms.Button();
            this.SledzenieListowe = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // list_logs
            // 
            this.list_logs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_id,
            this.col_IP,
            this.col_ms,
            this.col_zwrot,
            this.col_ttl});
            this.list_logs.Location = new System.Drawing.Point(12, 14);
            this.list_logs.Name = "list_logs";
            this.list_logs.Size = new System.Drawing.Size(559, 342);
            this.list_logs.TabIndex = 0;
            this.list_logs.UseCompatibleStateImageBehavior = false;
            this.list_logs.View = System.Windows.Forms.View.Details;
            // 
            // col_id
            // 
            this.col_id.Text = "Id";
            this.col_id.Width = 30;
            // 
            // col_IP
            // 
            this.col_IP.Text = "IP";
            this.col_IP.Width = 350;
            // 
            // col_ms
            // 
            this.col_ms.Text = "Zwrot";
            this.col_ms.Width = 70;
            // 
            // col_zwrot
            // 
            this.col_zwrot.Text = "ms";
            this.col_zwrot.Width = 50;
            // 
            // col_ttl
            // 
            this.col_ttl.Text = "Ttl";
            this.col_ttl.Width = 40;
            // 
            // tx_addres
            // 
            this.tx_addres.Location = new System.Drawing.Point(580, 30);
            this.tx_addres.Name = "tx_addres";
            this.tx_addres.Size = new System.Drawing.Size(108, 20);
            this.tx_addres.TabIndex = 1;
            // 
            // bt_sledz
            // 
            this.bt_sledz.Location = new System.Drawing.Point(580, 95);
            this.bt_sledz.Name = "bt_sledz";
            this.bt_sledz.Size = new System.Drawing.Size(108, 22);
            this.bt_sledz.TabIndex = 2;
            this.bt_sledz.Text = "Sledz Pakiet";
            this.bt_sledz.UseVisualStyleBackColor = true;
            this.bt_sledz.Click += new System.EventHandler(this.bt_sledz_Click);
            // 
            // Adres
            // 
            this.Adres.AutoSize = true;
            this.Adres.Location = new System.Drawing.Point(580, 14);
            this.Adres.Name = "Adres";
            this.Adres.Size = new System.Drawing.Size(37, 13);
            this.Adres.TabIndex = 3;
            this.Adres.Text = "Adres:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(577, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Wiadomosc w pakiecie:";
            // 
            // tx_msg
            // 
            this.tx_msg.Location = new System.Drawing.Point(580, 69);
            this.tx_msg.Name = "tx_msg";
            this.tx_msg.Size = new System.Drawing.Size(108, 20);
            this.tx_msg.TabIndex = 4;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(583, 141);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(580, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Czas oczekiwania[ms]:";
            // 
            // Sledzenie
            // 
            this.Sledzenie.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Sledzenie_DoWork);
            // 
            // bt_log
            // 
            this.bt_log.Location = new System.Drawing.Point(580, 167);
            this.bt_log.Name = "bt_log";
            this.bt_log.Size = new System.Drawing.Size(108, 25);
            this.bt_log.TabIndex = 8;
            this.bt_log.Text = "Zapisz logi";
            this.bt_log.UseVisualStyleBackColor = true;
            this.bt_log.Click += new System.EventHandler(this.bt_log_Click);
            // 
            // bt_infoo
            // 
            this.bt_infoo.Location = new System.Drawing.Point(613, 333);
            this.bt_infoo.Name = "bt_infoo";
            this.bt_infoo.Size = new System.Drawing.Size(84, 23);
            this.bt_infoo.TabIndex = 9;
            this.bt_infoo.Text = "Pomoc";
            this.bt_infoo.UseVisualStyleBackColor = true;
            this.bt_infoo.Click += new System.EventHandler(this.bt_infoo_Click);
            // 
            // box_d
            // 
            this.box_d.AutoSize = true;
            this.box_d.Location = new System.Drawing.Point(580, 198);
            this.box_d.Name = "box_d";
            this.box_d.Size = new System.Drawing.Size(88, 17);
            this.box_d.TabIndex = 10;
            this.box_d.Text = "Bez Adresow";
            this.box_d.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(645, 234);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown2.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(572, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Maksymalna liczba";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(572, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "przeskoków:";
            // 
            // bt_routetrack
            // 
            this.bt_routetrack.Location = new System.Drawing.Point(713, 234);
            this.bt_routetrack.Name = "bt_routetrack";
            this.bt_routetrack.Size = new System.Drawing.Size(102, 22);
            this.bt_routetrack.TabIndex = 14;
            this.bt_routetrack.Text = "Sledz z droga";
            this.bt_routetrack.UseVisualStyleBackColor = true;
            this.bt_routetrack.Click += new System.EventHandler(this.bt_routetrack_Click);
            // 
            // lista_ip
            // 
            this.lista_ip.FormattingEnabled = true;
            this.lista_ip.Location = new System.Drawing.Point(713, 30);
            this.lista_ip.Name = "lista_ip";
            this.lista_ip.Size = new System.Drawing.Size(102, 134);
            this.lista_ip.TabIndex = 15;
            this.lista_ip.SelectedIndexChanged += new System.EventHandler(this.lista_ip_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(710, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Lista Preferowana";
            // 
            // text_ip
            // 
            this.text_ip.Location = new System.Drawing.Point(712, 167);
            this.text_ip.Name = "text_ip";
            this.text_ip.Size = new System.Drawing.Size(103, 20);
            this.text_ip.TabIndex = 17;
            this.text_ip.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // bt_add_listpref
            // 
            this.bt_add_listpref.Location = new System.Drawing.Point(712, 193);
            this.bt_add_listpref.Name = "bt_add_listpref";
            this.bt_add_listpref.Size = new System.Drawing.Size(54, 22);
            this.bt_add_listpref.TabIndex = 18;
            this.bt_add_listpref.Text = "Dodaj";
            this.bt_add_listpref.UseVisualStyleBackColor = true;
            this.bt_add_listpref.Click += new System.EventHandler(this.bt_add_listpref_Click);
            // 
            // bt_del_list
            // 
            this.bt_del_list.Location = new System.Drawing.Point(772, 193);
            this.bt_del_list.Name = "bt_del_list";
            this.bt_del_list.Size = new System.Drawing.Size(43, 22);
            this.bt_del_list.TabIndex = 19;
            this.bt_del_list.Text = "Usuń";
            this.bt_del_list.UseVisualStyleBackColor = true;
            this.bt_del_list.Click += new System.EventHandler(this.bt_del_list_Click);
            // 
            // SledzenieListowe
            // 
            this.SledzenieListowe.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SledzenieListowe_DoWork);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(727, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "O autorze";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(827, 368);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bt_del_list);
            this.Controls.Add(this.bt_add_listpref);
            this.Controls.Add(this.text_ip);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lista_ip);
            this.Controls.Add(this.bt_routetrack);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.box_d);
            this.Controls.Add(this.bt_infoo);
            this.Controls.Add(this.bt_log);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tx_msg);
            this.Controls.Add(this.Adres);
            this.Controls.Add(this.bt_sledz);
            this.Controls.Add(this.tx_addres);
            this.Controls.Add(this.list_logs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Traceroute by Raik";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tx_adress;
        private System.Windows.Forms.Button bt_start;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_info;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader ms;
        private System.Windows.Forms.ColumnHeader zwrot;
        private System.Windows.Forms.ColumnHeader Ttl;
        private System.Windows.Forms.ListView list_logs;
        private System.Windows.Forms.ColumnHeader col_id;
        private System.Windows.Forms.ColumnHeader col_IP;
        private System.Windows.Forms.ColumnHeader col_ms;
        private System.Windows.Forms.ColumnHeader col_zwrot;
        private System.Windows.Forms.ColumnHeader col_ttl;
        private System.Windows.Forms.TextBox tx_addres;
        private System.Windows.Forms.Button bt_sledz;
        private System.Windows.Forms.Label Adres;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tx_msg;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker Sledzenie;
        private System.Windows.Forms.Button bt_log;
        private System.Windows.Forms.Button bt_infoo;
        private System.Windows.Forms.CheckBox box_d;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bt_routetrack;
        private System.Windows.Forms.ListBox lista_ip;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox text_ip;
        private System.Windows.Forms.Button bt_add_listpref;
        private System.Windows.Forms.Button bt_del_list;
        private System.ComponentModel.BackgroundWorker SledzenieListowe;
        private System.Windows.Forms.Button button1;
    }
}

