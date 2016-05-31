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
using System.Security.Cryptography;
using System.Diagnostics;

namespace Marschlu
{
    public partial class Marschlu : Form
    {
        public delegate void DoWorkDelegate();
        WebClient Verbindung = new WebClient();


        private void Startbtn_MouseHover(object sender, EventArgs e)
        {
            // when we hoover the button we get this
            Startbtn.Image = Properties.Resources.Btn2;
        }
        private void Startbtn_MouseLeave(object sender, EventArgs e)
        {
            // when we again leave the button we get back original color
            Startbtn.Image = Properties.Resources.Btn;
        }

        public Marschlu()
        {
            InitializeComponent();
        }

        public static string GetMD5Hash(string TextToHash)
        {
            if (string.IsNullOrEmpty(TextToHash) | TextToHash.Length == 0)
            {
                return string.Empty;
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] toHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(toHash);

            return System.BitConverter.ToString(result);
        }

        public void Marschlu_Load(object sender, EventArgs e)
        {
            this.Testa();
            //BeginInvoke(new DoWorkDelegate(Testa));
            
        }

        
        public void Testa()
        {
                int u = 1;
                int c = Convert.ToInt32(server.Text) + u;
                try
                {
                    //Es wird ein neues Update gestartet
                    if (Convert.ToInt32(your.Text) < Convert.ToInt32(server.Text))
                    {
                            Verbindung.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                            Verbindung.DownloadFileCompleted += delegate
                            {
                                try
                                {
                                    //Process oProcess = null;
                                    var oProcess = Process.Start("UnRAR.exe", "x -y -ac " + your.Text + ".rar");
                                    //extract.extract()
                                    //Patch löschen
                                    oProcess.WaitForExit();
                                    File.Delete(your.Text + ".rar");

                                    int ergebnis = Convert.ToInt32(your.Text) + u;
                                    your.Text = ergebnis.ToString();
                                    string Client = @"Client.dat";
                                    File.WriteAllText(Client, your.Text);
                                    this.Testa();
                                }
                                catch
                                {

                                }
                            };
                            // Start downloading the file

                            Verbindung.DownloadFileAsync(new Uri("http://127.0.0.1/patcher/data/" + your.Text + ".rar"), your.Text + ".rar");

                        

                    }
                    else if (your.Text == server.Text)
                    {
                        string tst = label1.Text;

                        if (Convert.ToUInt32(your.Text) > Convert.ToUInt32(tst))
                        {
                        Verbindung.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                        Verbindung.DownloadFileCompleted += delegate
                        {

                            try
                            {
                                //Process oProcess = null;
                                var oProcess = Process.Start("UnRAR.exe", "x -y -ac " + your.Text + ".rar");
                                //extract.extract()
                                //Patch löschen
                                oProcess.WaitForExit();
                                File.Delete(your.Text + ".rar");

                                int ergebnis = Convert.ToInt32(your.Text) + u;
                                your.Text = ergebnis.ToString();

                                string Client = @"Client.dat";
                                File.WriteAllText(Client, your.Text);
                                this.Testa();
                            }
                            catch
                            {

                            }
                        };
                        // Start downloading the file
                        Verbindung.DownloadFileAsync(new Uri("http://127.0.0.1/patcher/data/" + server.Text + ".rar"), server.Text + ".rar");
                        }

                        else
                        {
                            your.Text = server.Text;

                            string Client = @"Client.dat";
                            File.WriteAllText(Client, your.Text);

                            Startbtn.Enabled = true;
                        }
                    }
                    else if (your.Text == c.ToString())
                    {
                        your.Text = server.Text;

                        string Client = @"Client.dat";
                        File.WriteAllText(Client, your.Text);

                        Startbtn.Enabled = true;
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                    
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Update the progressbar percentage only when the value is not the same.
            progressBar1.Value = e.ProgressPercentage;
            
            // Show the percentage on our label.
            labelPerc.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        private void Startbtn_Click_1(object sender, EventArgs e)
        {
            Process.Start("AbsoluteZero.exe");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Option option = new Option();

            option.Show();
        }

        private void Homepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://127.0.0.1/register.php");
        }

        private void Homepage_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://127.0.0.1");
        }


    }
}
