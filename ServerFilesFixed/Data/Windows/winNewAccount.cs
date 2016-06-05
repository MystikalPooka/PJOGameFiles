﻿/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Text;

using SdlDotNet.Widgets;
using System.Drawing;
using Client.Logic.Network;

namespace Client.Logic.Windows
{
    class winNewAccount : Core.WindowCore
    {
        Label lblCreateNewAccount;
        Label lblBack;
        Label lblAccountName;
        Label lblPassword;
        Label lblRetypePassword;
        TextBox txtAccountName;
        TextBox txtPassword;
        TextBox txtRetypePassword;

        public winNewAccount()
            : base("winNewAccount") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "New Account";
            this.TitleBar.CloseButton.Visible = false;
            this.BackgroundImage = Skins.SkinManager.LoadGui("New Account");
            this.Size = this.BackgroundImage.Size;
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            lblBack = new Label("btnBack");
            lblBack.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblBack.Location = new Point(147, 355);
            lblBack.AutoSize = true;
            lblBack.ForeColor = Color.Black;
            lblBack.Text = "Back to Login Screen";
            lblBack.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblBack_Click);

            lblCreateNewAccount = new Label("lblCreateNewAccount");
            lblCreateNewAccount.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblCreateNewAccount.Location = new Point(23, 200);
            lblCreateNewAccount.AutoSize = true;
            lblCreateNewAccount.ForeColor = Color.Black;
            lblCreateNewAccount.Text = "Create New Account";
            lblCreateNewAccount.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblCreateNewAccount_Click);

            lblAccountName = new Label("lblAccountName");
            lblAccountName.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblAccountName.Location = new Point(28, 90);
            lblAccountName.AutoSize = true;
            lblAccountName.ForeColor = Color.Black;
            lblAccountName.Text = "Desired Account";

            lblPassword = new Label("lblPassword");
            lblPassword.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblPassword.Location = new Point(28, 122);
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.Black;
            lblPassword.Text = "Account Password:";

            lblRetypePassword = new Label("lblRetypePassword");
            lblRetypePassword.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblRetypePassword.Location = new Point(28, 154);
            lblRetypePassword.AutoSize = true;
            lblRetypePassword.ForeColor = Color.Black;
            lblRetypePassword.Text = "Retype Password:";

            txtAccountName = new TextBox("txtAccountName");
            txtAccountName.Size = new System.Drawing.Size(177, 16);
            txtAccountName.Location = new Point(25, 109);

            txtPassword = new TextBox("txtPassword");
            txtPassword.Size = new System.Drawing.Size(177, 16);
            txtPassword.Location = new Point(25, 140);

            txtRetypePassword = new TextBox("txtRetypePassword");
            txtRetypePassword.Size = new System.Drawing.Size(177, 16);
            txtRetypePassword.Location = new Point(25, 174);

            this.AddWidget(lblBack);
            this.AddWidget(lblCreateNewAccount);
            this.AddWidget(lblAccountName);
            this.AddWidget(lblPassword);
            this.AddWidget(lblRetypePassword);
            this.AddWidget(txtAccountName);
            this.AddWidget(txtPassword);
            this.AddWidget(txtRetypePassword);

            this.LoadComplete();
        }

        void lblBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowSwitcher.ShowMainMenu();
        }

        void lblCreateNewAccount_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            string account = txtAccountName.Text;
            string password = Security.Hash.GenerateMD5Hash(txtPassword.Text);


            if (NetworkManager.TcpClient.Socket.Connected) {
                Messenger.SendCreateAccountRequest(account, password);
                this.Close();
                WindowSwitcher.AddWindow(new winLoading());
                ((Windows.winLoading)WindowManager.FindWindow("winLoading")).UpdateLoadText("Creating account...");
            } else {
                this.Close();
                WindowSwitcher.ShowMainMenu();
                SdlDotNet.Widgets.MessageBox.Show("You are not connected to the Server!", "----");
            }
        }
    }
}
