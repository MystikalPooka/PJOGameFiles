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


namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Graphics;

    using SdlDotNet.Widgets;
    using Client.Logic.Widgets;

    class mnuJobList : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        #region Fields

        const int MAX_ITEMS = 7;

        Logic.Widgets.MenuItemPicker itemPicker;
        Label lblJobList;
        MissionTitle[] items;

        #endregion Fields

        #region Constructors

        public mnuJobList(string name)
            : base(name) {
            base.Size = new Size(280, 460);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(15, 10);

            itemPicker = new Logic.Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(20, 64);

            lblJobList = new Label("lblJobList");
            lblJobList.AutoSize = true;
            lblJobList.Font = FontManager.LoadFont("PMU", 48);
            lblJobList.ForeColor = Color.WhiteSmoke;
            lblJobList.Text = "Job List";
            lblJobList.Location = new Point(20, 0);

            items = new MissionTitle[8];
            int lastY = 58;
            for (int i = 0; i < items.Length; i++) {
                items[i] = new MissionTitle("item" + i, this.Width);
                items[i].Location = new Point(15, lastY);

                if (Players.PlayerManager.MyPlayer.JobList.Jobs.Count > i) {
                    items[i].SetJob(Players.PlayerManager.MyPlayer.JobList.Jobs[i]);
                } else {
                    items[i].SetJob(null);
                }

                this.AddWidget(items[i]);

                lastY += items[i].Height + 8;
            }

            this.AddWidget(itemPicker);
            this.AddWidget(lblJobList);
        }

        #endregion Constructors

        #region Properties

        public Logic.Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public bool Modal {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public void DisplayItems() {
            for (int i = 0; i < items.Length; i++) {
                if (Players.PlayerManager.MyPlayer.JobList.Jobs.Count > i) {
                    items[i].SetJob(Players.PlayerManager.MyPlayer.JobList.Jobs[i]);
                } else {
                    items[i].SetJob(null);
                }
            }
            Menus.Core.IMenu mnuJobSelected = Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuJobSelected");
            if (mnuJobSelected != null) {
                Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(mnuJobSelected);
            }
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(20, 64 + ((items[0].Height + 8) * itemNum));
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
                        // Show the main menu when the backspace key is pressed
                        MenuSwitcher.ShowMainMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum) {
            if (Players.PlayerManager.MyPlayer.JobList.Jobs.Count > itemNum) {
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuJobSelected("mnuJobSelected", itemNum));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuJobSelected");
                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            }
        }

        #endregion Methods
    }
}