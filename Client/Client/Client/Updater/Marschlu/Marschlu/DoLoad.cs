using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Marschlu
{
    public partial class DoLoad : Form
    {
        public delegate void DoWorkDelegate();
        public DoLoad()
        {
            InitializeComponent();
        }

        public void Load_Load(object sender, EventArgs e)
        {
            BeginInvoke(new DoWorkDelegate(LoadVersion));
        }

        public void LoadVersion()
        {
            WebClient Verbindung = new WebClient();

            Marschlu patch = new Marschlu();

            string line;
            string ServerVersion = Verbindung.DownloadString("http://127.0.0.1/patcher/version.txt");
            string NeedVersion = Verbindung.DownloadString("http://127.0.0.1/patcher/need.txt");
            string Client = @"Client.dat";

            Verbindung.DownloadFile("http://127.0.0.1/patcher/UnRAR.exe", "UnRAR.exe");


            if (File.Exists(Client))
            {
                System.IO.StreamReader file =
                new System.IO.StreamReader(Client);
                while ((line = file.ReadLine()) != null)
                {
                    patch.label1.Text = line;
                    label1.Text = line;
                }
                file.Close();

                string yourVers = label1.Text;


                if (Convert.ToInt32(yourVers) < Convert.ToInt32(NeedVersion))
                {
                    Verbindung.DownloadFile("http://127.0.0.1/patcher/need.txt", "Client.dat");

                    patch.server.Text = ServerVersion;
                    patch.your.Text = NeedVersion;
                    patch.label1.Text = NeedVersion;

                    this.Hide();
                    patch.Show();
                }
                else if (Convert.ToInt32(yourVers) >= Convert.ToInt32(NeedVersion))
                {
                    patch.server.Text = ServerVersion;
                    patch.your.Text = yourVers;

                    this.Hide();
                    patch.Show();
                }
            }
            else
            {

                Verbindung.DownloadFile("http://127.0.0.1/patcher/need.txt", "Client.dat");

                patch.server.Text = ServerVersion;
                patch.your.Text = NeedVersion;
                patch.label1.Text = NeedVersion;

                this.Hide();
                patch.Show();
            }
        }
    }
}
