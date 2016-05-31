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
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;
using PMU.Core;
using SdlDotNet.Widgets;
using Client.Logic.Network;
using Client.Logic.Tournaments;


namespace Client.Logic.Menus
{
    class mnuTournamentListingSelected : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        Label lblJoin;
        Label lblViewRules;
        Widgets.MenuItemPicker itemPicker;
        const int MAX_ITEMS = 1;
        TournamentListing selectedListing;
        Enums.TournamentListingMode mode;

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuTournamentListingSelected(string name, TournamentListing selectedListing, Enums.TournamentListingMode mode)
            : base(name) {
            this.selectedListing = selectedListing;
            this.mode = mode;

            base.Size = new Size(185, 125);

            base.MenuDirection = Enums.MenuDirection.Horizontal;
            base.Location = new Point(335, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            lblJoin = new Label("lblJoin");
            lblJoin.Font = FontManager.LoadFont("PMU", 32);
            lblJoin.AutoSize = true;
            if (mode == Enums.TournamentListingMode.Join) {
                lblJoin.Text = "Join";
            } else if (mode == Enums.TournamentListingMode.Spectate) {
                lblJoin.Text = "Spectate";
            }
            lblJoin.Location = new Point(30, 8);
            lblJoin.HoverColor = Color.Red;
            lblJoin.ForeColor = Color.WhiteSmoke;
            lblJoin.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblJoin_Click);

            lblViewRules = new Label("lblViewRules");
            lblViewRules.Font = FontManager.LoadFont("PMU", 32);
            lblViewRules.AutoSize = true;
            lblViewRules.Text = "View Rules";
            lblViewRules.Location = new Point(30, 58);
            lblViewRules.HoverColor = Color.Red;
            lblViewRules.ForeColor = Color.WhiteSmoke;
            lblViewRules.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblViewRules_Click);

            this.AddWidget(itemPicker);
            this.AddWidget(lblJoin);
            this.AddWidget(lblViewRules);
        }

        void lblViewRules_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1);
        }


        void lblJoin_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0);
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 23 + (50 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem == MAX_ITEMS) {
                            ChangeSelected(0);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem == 0) {
                            ChangeSelected(MAX_ITEMS);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectItem(itemPicker.SelectedItem);
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }


        private void SelectItem(int selectedItem) {
            switch (selectedItem) {
                case 0: { // Join tournament / Spectate
                    if (mode == Enums.TournamentListingMode.Join) {
                        Messenger.SendJoinTournament(selectedListing.TournamentID);
                    } else if (mode == Enums.TournamentListingMode.Spectate) {
                        Messenger.SendSpectateTournament(selectedListing.TournamentID);
                    }
                        MenuSwitcher.CloseAllMenus();
                    }
                    break;
                case 1: { // View tournament rules
                        Messenger.SendViewTournamentRules(selectedListing.TournamentID);
                        MenuSwitcher.CloseAllMenus();
                    }
                    break;
            }
        }

        private void CloseMenu() {
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
        }

    }
}
