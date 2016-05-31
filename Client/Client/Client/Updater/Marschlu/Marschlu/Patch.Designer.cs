namespace Marschlu
{
    partial class Marschlu
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Marschlu));
            this.Pass = new System.Windows.Forms.TextBox();
            this.User = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.UrVers = new System.Windows.Forms.Label();
            this.ServVers = new System.Windows.Forms.Label();
            this.your = new System.Windows.Forms.Label();
            this.server = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.labelPerc = new System.Windows.Forms.Label();
            this.labelDownloaded = new System.Windows.Forms.Label();
            this.News = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.Option = new System.Windows.Forms.LinkLabel();
            this.Startbtn = new System.Windows.Forms.PictureBox();
            this.Register = new System.Windows.Forms.LinkLabel();
            this.Homepage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.Startbtn)).BeginInit();
            this.SuspendLayout();
            // 
            // Pass
            // 
            this.Pass.Location = new System.Drawing.Point(0, 0);
            this.Pass.Name = "Pass";
            this.Pass.Size = new System.Drawing.Size(100, 20);
            this.Pass.TabIndex = 22;
            // 
            // User
            // 
            this.User.Location = new System.Drawing.Point(0, 0);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(100, 20);
            this.User.TabIndex = 21;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.SystemColors.Info;
            this.progressBar1.Location = new System.Drawing.Point(21, 384);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(586, 21);
            this.progressBar1.TabIndex = 0;
            // 
            // UrVers
            // 
            this.UrVers.AutoSize = true;
            this.UrVers.BackColor = System.Drawing.Color.Transparent;
            this.UrVers.ForeColor = System.Drawing.Color.White;
            this.UrVers.Location = new System.Drawing.Point(380, 221);
            this.UrVers.Name = "UrVers";
            this.UrVers.Size = new System.Drawing.Size(67, 13);
            this.UrVers.TabIndex = 0;
            this.UrVers.Text = "Your Version";
            // 
            // ServVers
            // 
            this.ServVers.AutoSize = true;
            this.ServVers.BackColor = System.Drawing.Color.Transparent;
            this.ServVers.ForeColor = System.Drawing.Color.White;
            this.ServVers.Location = new System.Drawing.Point(380, 194);
            this.ServVers.Name = "ServVers";
            this.ServVers.Size = new System.Drawing.Size(76, 13);
            this.ServVers.TabIndex = 0;
            this.ServVers.Text = "Server Version";
            // 
            // your
            // 
            this.your.AutoSize = true;
            this.your.BackColor = System.Drawing.Color.Transparent;
            this.your.ForeColor = System.Drawing.Color.White;
            this.your.Location = new System.Drawing.Point(456, 221);
            this.your.Name = "your";
            this.your.Size = new System.Drawing.Size(10, 13);
            this.your.TabIndex = 4;
            this.your.Text = "-";
            // 
            // server
            // 
            this.server.AutoSize = true;
            this.server.BackColor = System.Drawing.Color.Transparent;
            this.server.ForeColor = System.Drawing.Color.White;
            this.server.Location = new System.Drawing.Point(456, 194);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(10, 13);
            this.server.TabIndex = 5;
            this.server.Text = "-";
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(0, 0);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 20;
            // 
            // labelPerc
            // 
            this.labelPerc.AutoSize = true;
            this.labelPerc.BackColor = System.Drawing.Color.Transparent;
            this.labelPerc.ForeColor = System.Drawing.Color.White;
            this.labelPerc.Location = new System.Drawing.Point(327, 370);
            this.labelPerc.Name = "labelPerc";
            this.labelPerc.Size = new System.Drawing.Size(10, 13);
            this.labelPerc.TabIndex = 9;
            this.labelPerc.Text = "-";
            // 
            // labelDownloaded
            // 
            this.labelDownloaded.AutoSize = true;
            this.labelDownloaded.Location = new System.Drawing.Point(165, 9);
            this.labelDownloaded.Name = "labelDownloaded";
            this.labelDownloaded.Size = new System.Drawing.Size(31, 13);
            this.labelDownloaded.TabIndex = 10;
            this.labelDownloaded.Text = "MB´s";
            this.labelDownloaded.Visible = false;
            // 
            // News
            // 
            this.News.AutoSize = true;
            this.News.BackColor = System.Drawing.Color.Transparent;
            this.News.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.News.ForeColor = System.Drawing.Color.White;
            this.News.Location = new System.Drawing.Point(25, 44);
            this.News.Name = "News";
            this.News.Size = new System.Drawing.Size(65, 25);
            this.News.TabIndex = 11;
            this.News.Text = "News";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(30, 72);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(328, 295);
            this.webBrowser1.TabIndex = 13;
            this.webBrowser1.Url = new System.Uri("http://absolutezeroonline.com/patcher/News.html", System.UriKind.Absolute);
            // 
            // Option
            // 
            this.Option.ActiveLinkColor = System.Drawing.Color.White;
            this.Option.AutoSize = true;
            this.Option.BackColor = System.Drawing.Color.Transparent;
            this.Option.DisabledLinkColor = System.Drawing.Color.White;
            this.Option.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Option.ForeColor = System.Drawing.Color.White;
            this.Option.LinkArea = new System.Windows.Forms.LinkArea(0, 7);
            this.Option.LinkColor = System.Drawing.Color.White;
            this.Option.Location = new System.Drawing.Point(478, 11);
            this.Option.Name = "Option";
            this.Option.Size = new System.Drawing.Size(43, 13);
            this.Option.TabIndex = 16;
            this.Option.TabStop = true;
            this.Option.Text = "Options";
            this.Option.VisitedLinkColor = System.Drawing.Color.White;
            this.Option.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Startbtn
            // 
            this.Startbtn.BackColor = System.Drawing.Color.Transparent;
            this.Startbtn.BackgroundImage = global::Marschlu.Properties.Resources.Btn;
            this.Startbtn.Location = new System.Drawing.Point(394, 72);
            this.Startbtn.Name = "Startbtn";
            this.Startbtn.Size = new System.Drawing.Size(166, 41);
            this.Startbtn.TabIndex = 17;
            this.Startbtn.TabStop = false;
            this.Startbtn.Click += new System.EventHandler(this.Startbtn_Click_1);
            this.Startbtn.MouseLeave += new System.EventHandler(this.Startbtn_MouseLeave);
            this.Startbtn.MouseHover += new System.EventHandler(this.Startbtn_MouseHover);
            // 
            // Register
            // 
            this.Register.ActiveLinkColor = System.Drawing.Color.White;
            this.Register.AutoSize = true;
            this.Register.BackColor = System.Drawing.Color.Transparent;
            this.Register.DisabledLinkColor = System.Drawing.Color.White;
            this.Register.LinkColor = System.Drawing.Color.White;
            this.Register.Location = new System.Drawing.Point(242, 11);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(46, 13);
            this.Register.TabIndex = 18;
            this.Register.TabStop = true;
            this.Register.Text = "Register";
            this.Register.VisitedLinkColor = System.Drawing.Color.White;
            this.Register.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Homepage_LinkClicked);
            // 
            // Homepage
            // 
            this.Homepage.ActiveLinkColor = System.Drawing.Color.White;
            this.Homepage.AutoSize = true;
            this.Homepage.BackColor = System.Drawing.Color.Transparent;
            this.Homepage.DisabledLinkColor = System.Drawing.Color.White;
            this.Homepage.LinkColor = System.Drawing.Color.White;
            this.Homepage.Location = new System.Drawing.Point(354, 12);
            this.Homepage.Name = "Homepage";
            this.Homepage.Size = new System.Drawing.Size(59, 13);
            this.Homepage.TabIndex = 19;
            this.Homepage.TabStop = true;
            this.Homepage.Text = "Homepage";
            this.Homepage.VisitedLinkColor = System.Drawing.Color.White;
            this.Homepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Homepage_LinkClicked_1);
            // 
            // Marschlu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Marschlu.Properties.Resources.LaucherVersuch;
            this.ClientSize = new System.Drawing.Size(625, 430);
            this.Controls.Add(this.Homepage);
            this.Controls.Add(this.Register);
            this.Controls.Add(this.Startbtn);
            this.Controls.Add(this.Option);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.News);
            this.Controls.Add(this.labelDownloaded);
            this.Controls.Add(this.labelPerc);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.server);
            this.Controls.Add(this.your);
            this.Controls.Add(this.ServVers);
            this.Controls.Add(this.UrVers);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.User);
            this.Controls.Add(this.Pass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Marschlu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marschlu";
            this.TransparencyKey = System.Drawing.SystemColors.ActiveCaption;
            this.Load += new System.EventHandler(this.Marschlu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Startbtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Pass;
        private System.Windows.Forms.TextBox User;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label UrVers;
        private System.Windows.Forms.Label ServVers;
        public System.Windows.Forms.Label your;
        public System.Windows.Forms.Label server;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Label labelPerc;
        private System.Windows.Forms.Label labelDownloaded;
        private System.Windows.Forms.Label News;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.LinkLabel Option;
        private System.Windows.Forms.PictureBox Startbtn;
        private System.Windows.Forms.LinkLabel Register;
        private System.Windows.Forms.LinkLabel Homepage;
    }
}

