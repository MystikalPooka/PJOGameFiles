/*The MIT License (MIT)

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

using System.Drawing;
using SdlDotNet.Widgets;

namespace Client.Logic.Windows
{
    class winCredits : Core.WindowCore
    {
        Label lblTeam;
        Label lblBack;
        Label lblProgramming;
        Label lblPikablu;
        Label lblBlaze;
        Label lblGraphics;
        Label lblLossetta;
        Label lblPernuta;
        Label lblZach;


        public winCredits()
            : base("WinCredits") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Credits";
            this.TitleBar.CloseButton.Visible = false;
            //this.BackgroundImage = Skins.SkinManager.LoadGui("Credits");
            //this.Size = this.BackgroundImage.Size;
            this.BackColor = Color.White;
            this.Size = new Size(400, 400);
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            lblTeam = new Label("lblTeam");
            lblTeam.Font = Graphics.FontManager.LoadFont("PMU", 30);
            lblTeam.AutoSize = true;
            lblTeam.Location = new Point(30, 20);
            lblTeam.Text = "Pokemon Journey Online Staff Team!";
            lblTeam.BackColor = Color.GreenYellow;

            lblBack = new Label("lblBack");
            lblBack.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblBack.AutoSize = true;
            lblBack.Location = new Point(0, 330);
            lblBack.Text = "Return to Login Screen";
            lblBack.BackColor = Color.Blue;
            lblBack.Click +=new EventHandler<MouseButtonEventArgs>(lblBack_Click);

            lblProgramming = new Label("lblProgramming");
            lblProgramming.Font = Graphics.FontManager.LoadFont("PMU", 25);
            lblProgramming.AutoSize = true;
            lblProgramming.Location = new Point(0, 80);
            lblProgramming.Text = "Programming:";
            lblProgramming.BackColor = Color.Silver;

            lblPikablu = new Label("lblPikablu");
            lblPikablu.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblPikablu.AutoSize = true;
            lblPikablu.Location = new Point(120, 85);
            lblPikablu.Text = "Pikablu";
            lblPikablu.BackColor = Color.Yellow;

            lblBlaze = new Label("lblBlaze");
            lblBlaze.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblBlaze.AutoSize = true;
            lblBlaze.Location = new Point(175, 85);
            lblBlaze.Text = "Blaze";
            lblBlaze.BackColor = Color.Red;
            // TODO: Add Forecolors for the Credits.
            //lblDarkmazer.BackColor = Color.Black; -For the Darkness, as always.
            //lblDarkmazer.ForeColor = Color.White; -Even in the deepest of darkness, there's always light...


            lblGraphics = new Label("lblGraphics");
            lblGraphics.Font = Graphics.FontManager.LoadFont("PMU", 25);
            lblGraphics.AutoSize = true;
            lblGraphics.Location = new Point(0, 140);
            lblGraphics.Text = "Graphics:";
            lblGraphics.BackColor = Color.Brown;

            lblLossetta = new Label("lblLossetta");
            lblLossetta.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblLossetta.AutoSize = true;
            lblLossetta.Location = new Point(90, 145);
            lblLossetta.Text = "Lossetta";
            lblLossetta.BackColor = Color.SkyBlue;

            lblZach = new Label("lblZach");
            lblZach.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblZach.AutoSize = true;
            lblZach.Location = new Point(130, 185);
            lblZach.Text = "Zach";
            lblZach.BackColor = Color.Purple;
            //lblZach.BackColor = Color.Black; -Shiny, shiny!
            //lblZach.ForeColor = Color.Silver; -Isn't Silver shiny though? Doesn't it count?


            this.AddWidget(lblTeam);
            this.AddWidget(lblBack);
            this.AddWidget(lblProgramming);
            this.AddWidget(lblPikablu);
            this.AddWidget(lblBlaze);
            this.AddWidget(lblGraphics);
            this.AddWidget(lblLossetta);
            this.AddWidget(lblZach);
            this.LoadComplete();
        }

        void lblBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowSwitcher.ShowMainMenu();
        }
    }
}
