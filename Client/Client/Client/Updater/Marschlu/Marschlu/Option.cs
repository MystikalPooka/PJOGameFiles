using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Marschlu
{
    public partial class Option : Form
    {
        WebClient Verbindung = new WebClient();
        string Resolution = @"ressystem\Option.mco";

        public Option()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient Verbindung = new WebClient();
            DoLoad Load = new DoLoad();
            string Client = @"Client.dat";
            string NeedVersion = Verbindung.DownloadString("http://192.151.148.190/patcher/need.txt");
            File.WriteAllText(Client, NeedVersion);

            Application.Restart();

        }

        private void Option_Load(object sender, EventArgs e)
        {
            
        }

        private void R1920_CheckedChanged(object sender, EventArgs e)
        {
            Verbindung.DownloadFile(new Uri("http://192.151.148.190/patcher/resolution/1920.mco"), Resolution);
        }

        private void R1600_CheckedChanged(object sender, EventArgs e)
        {
            Verbindung.DownloadFile(new Uri("http://192.151.148.190/patcher/resolution/1600.mco"), Resolution);
        }

        private void R1280_CheckedChanged(object sender, EventArgs e)
        {
            Verbindung.DownloadFile(new Uri("http://192.151.148.190/patcher/resolution/1280.mco"), Resolution);
        }


    }
}
