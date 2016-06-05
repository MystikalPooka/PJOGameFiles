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
    class winDeleteAccount : Core.WindowCore
    {
        Label lblName;
        Label lblPassword;
        Label lblDelete;
        Label lblBack;
        TextBox txtName;
        TextBox txtPassword;

        public winDeleteAccount()
            : base("winDeleteAccount") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Delete Account";
            this.TitleBar.CloseButton.Visible = false;
            this.BackgroundImage = Skins.SkinManager.LoadGui("Delete Account");
            this.Size = this.BackgroundImage.Size;
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            lblBack = new Label("lblBack");
            lblBack.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblBack.Location = new Point(140, 355);
            lblBack.AutoSize = true;
            lblBack.ForeColor = Color.Black;
            lblBack.Text = "Back to Login Screen";
            lblBack.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblBack_Click);

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblName.Location = new Point(28, 97);
            lblName.AutoSize = true;
            lblName.ForeColor = Color.Black;
            lblName.Text = "Account Name";

            lblPassword = new Label("lblPassword");
            lblPassword.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblPassword.Location = new Point(32, 143);
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.Black;
            lblPassword.Text = "Password";

            lblDelete = new Label("lblDelete");
            lblDelete.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblDelete.Location = new Point(35, 190);
            lblDelete.AutoSize = true;
            lblDelete.ForeColor = Color.Black;
            lblDelete.Text = "Delete Account";
            lblDelete.Click +=new EventHandler<MouseButtonEventArgs>(lblDelete_Click);

            txtName = new TextBox("txtName");
            txtName.Size = new System.Drawing.Size(165, 16);
            txtName.Location = new Point(30, 118);

            txtPassword = new TextBox("txtPassword");
            txtPassword.Size = new System.Drawing.Size(165, 16);
            txtPassword.Location = new Point(30, 164);

            this.AddWidget(lblBack);
            this.AddWidget(lblName);
            this.AddWidget(lblPassword);
            this.AddWidget(lblDelete);
            this.AddWidget(txtName);
            this.AddWidget(txtPassword);

            this.LoadComplete();
        }

        void lblBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowSwitcher.ShowMainMenu();
        }

        void lblDelete_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            string account = txtName.Text;
            string password = txtPassword.Text;

            if (NetworkManager.TcpClient.Socket.Connected) {
                Messenger.SendDeleteAccount(account, password);
                this.Close();
                WindowSwitcher.AddWindow(new winLoading());
                ((Windows.winLoading)WindowManager.FindWindow("winLoading")).UpdateLoadText("Deleting account...");
            } else {
                this.Close();
                WindowSwitcher.ShowMainMenu();
                SdlDotNet.Widgets.MessageBox.Show("You are not connected to the Server!", "----");
            }
        }
    }
}
