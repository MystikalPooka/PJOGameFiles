﻿using System;
using System.Collections.Generic;

using System.Drawing;
using System.Text;

using SdlDotNet.Widgets;

namespace Client.Logic.Windows
{
    class winOptions2 : Core.WindowCore
    {

        #region Fields

        const int MAX_ITEMS = 12;

        Widgets.MenuItemPicker itemPicker;
        Label lblOptions;
        Label lblPlayerData;
        Label lblPlayerDataName;
        Label lblPlayerDataDamage;
        Label lblPlayerDataMiniHP;
        Label lblPlayerDataAutoSaveSpeed;
        Label lblNpcData;
        Label lblNpcDataName;
        Label lblNpcDataDamage;
        Label lblNpcDataMiniHP;
        Label lblSoundData;
        Label lblSoundDataMusic;
        Label lblSoundDataSound;
        Label lblChatData;
        Label lblChatDataSpeechBubbles;
        Label lblChatDataTimeStamps;
        Label lblChatDataAutoScroll;
        Label lblSave;

        bool[] tempOptions;
        int tempAutoSaveSpeed;

        #endregion Fields

        #region Constructors

        public winOptions2()
            : base("winOptions")
        {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Options";
            this.TitleBar.CloseButton.Visible = false;
            this.BackColor = Color.Blue;
            //this.BackgroundImage = Skins.SkinManager.LoadGui("Options");
            this.Size = new Size(280, 390);
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(30, 83);

            lblOptions = new Label("lblOptions");
            lblOptions.Location = new Point(20, 0);
            lblOptions.AutoSize = true;
            lblOptions.Font = Graphics.FontManager.LoadFont("PMU", 48);
            lblOptions.Text = "Options";

            lblPlayerData = new Label("lblPlayerData");
            lblPlayerData.Location = new Point(30, 48);
            lblPlayerData.AutoSize = true;
            lblPlayerData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblPlayerData.Text = "Player Data";

            lblPlayerDataName = new Label("lblPlayerDataName");
            lblPlayerDataName.Location = new Point(40, 78);
            lblPlayerDataName.AutoSize = true;
            lblPlayerDataName.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblPlayerDataName.Text = "Name: ";
            lblPlayerDataName.HoverColor = Color.Red;

            lblPlayerDataDamage = new Label("lblPlayerDataDamage");
            lblPlayerDataDamage.Location = new Point(40, 92);
            lblPlayerDataDamage.AutoSize = true;
            lblPlayerDataDamage.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblPlayerDataDamage.Text = "Damage: ";
            lblPlayerDataDamage.HoverColor = Color.Red;

            lblPlayerDataMiniHP = new Label("lblPlayerDataMiniHP");
            lblPlayerDataMiniHP.Location = new Point(40, 106);
            lblPlayerDataMiniHP.AutoSize = true;
            lblPlayerDataMiniHP.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblPlayerDataMiniHP.Text = "Mini-HP: ";
            lblPlayerDataMiniHP.HoverColor = Color.Red;

            lblPlayerDataAutoSaveSpeed = new Label("lblPlayerDataAutoSaveSpeed");
            lblPlayerDataAutoSaveSpeed.Location = new Point(40, 120);
            lblPlayerDataAutoSaveSpeed.AutoSize = true;
            lblPlayerDataAutoSaveSpeed.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblPlayerDataAutoSaveSpeed.Text = "Auto-Save Speed: ";
            lblPlayerDataAutoSaveSpeed.HoverColor = Color.Red;


            lblNpcData = new Label("lblNpcData");
            lblNpcData.Location = new Point(30, 130);
            lblNpcData.AutoSize = true;
            lblNpcData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblNpcData.Text = "NPC Data";

            lblNpcDataName = new Label("lblNpcDataName");
            lblNpcDataName.Location = new Point(40, 160);
            lblNpcDataName.AutoSize = true;
            lblNpcDataName.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblNpcDataName.Text = "Name: ";
            lblNpcDataName.HoverColor = Color.Red;

            lblNpcDataDamage = new Label("lblNpcDataDamage");
            lblNpcDataDamage.Location = new Point(40, 174);
            lblNpcDataDamage.AutoSize = true;
            lblNpcDataDamage.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblNpcDataDamage.Text = "Damage: ";
            lblNpcDataDamage.HoverColor = Color.Red;

            lblNpcDataMiniHP = new Label("lblNpcDataMiniHP");
            lblNpcDataMiniHP.Location = new Point(40, 188);
            lblNpcDataMiniHP.AutoSize = true;
            lblNpcDataMiniHP.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblNpcDataMiniHP.Text = "Mini-HP: ";
            lblNpcDataMiniHP.HoverColor = Color.Red;


            lblSoundData = new Label("lblSoundData");
            lblSoundData.Location = new Point(30, 198);
            lblSoundData.AutoSize = true;
            lblSoundData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblSoundData.Text = "Sound Data";

            lblSoundDataMusic = new Label("lblSoundDataMusic");
            lblSoundDataMusic.Location = new Point(40, 228);
            lblSoundDataMusic.AutoSize = true;
            lblSoundDataMusic.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblSoundDataMusic.Text = "Music: ";
            lblSoundDataMusic.HoverColor = Color.Red;

            lblSoundDataSound = new Label("lblSoundDataSound");
            lblSoundDataSound.Location = new Point(40, 242);
            lblSoundDataSound.AutoSize = true;
            lblSoundDataSound.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblSoundDataSound.Text = "Sound: ";
            lblSoundDataSound.HoverColor = Color.Red;


            lblChatData = new Label("lblChatData");
            lblChatData.Location = new Point(30, 252);
            lblChatData.AutoSize = true;
            lblChatData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblChatData.Text = "Chat Data";

            lblChatDataSpeechBubbles = new Label("lblChatDataSpeechBubbles");
            lblChatDataSpeechBubbles.Location = new Point(40, 282);
            lblChatDataSpeechBubbles.AutoSize = true;
            lblChatDataSpeechBubbles.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblChatDataSpeechBubbles.Text = "Speech Bubbles: ";
            lblChatDataSpeechBubbles.HoverColor = Color.Red;

            lblChatDataTimeStamps = new Label("lblChatDataTimeStamps");
            lblChatDataTimeStamps.Location = new Point(40, 296);
            lblChatDataTimeStamps.AutoSize = true;
            lblChatDataTimeStamps.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblChatDataTimeStamps.Text = "TimeStamps: ";
            lblChatDataTimeStamps.HoverColor = Color.Red;

            lblChatDataAutoScroll = new Label("lblChatDataAutoScroll");
            lblChatDataAutoScroll.Location = new Point(40, 310);
            lblChatDataAutoScroll.AutoSize = true;
            lblChatDataAutoScroll.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblChatDataAutoScroll.Text = "Auto-Scroll: ";
            lblChatDataAutoScroll.HoverColor = Color.Red;


            lblSave = new Label("lblSave");
            lblSave.Location = new Point(30, 330);
            lblSave.AutoSize = true;
            lblSave.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblSave.Text = "Save";
            lblSave.HoverColor = Color.Red;
            lblSave.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSave_Click);


            this.AddWidget(itemPicker);

            this.AddWidget(lblOptions);

            this.AddWidget(lblPlayerData);
            this.AddWidget(lblPlayerDataName);
            this.AddWidget(lblPlayerDataDamage);
            this.AddWidget(lblPlayerDataMiniHP);
            this.AddWidget(lblPlayerDataAutoSaveSpeed);

            this.AddWidget(lblNpcData);
            this.AddWidget(lblNpcDataName);
            this.AddWidget(lblNpcDataDamage);
            this.AddWidget(lblNpcDataMiniHP);

            this.AddWidget(lblSoundData);
            this.AddWidget(lblSoundDataMusic);
            this.AddWidget(lblSoundDataSound);


            this.AddWidget(lblChatData);
            this.AddWidget(lblChatDataSpeechBubbles);
            this.AddWidget(lblChatDataTimeStamps);
            this.AddWidget(lblChatDataAutoScroll);

            this.AddWidget(lblSave);

            tempOptions = new bool[12];
            tempAutoSaveSpeed = new int();



            for (int i = 0; i < 12; i++)
            {
                CreateTempOption(i);
                ShowOption(i);
            }
        }


        void lblSave_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(12);
        }


        #endregion Constructors

        #region Methods

        public void ChangeSelected(int itemNum)
        {
            int pointerX = 30;
            int pointerY = 83;

            if (itemNum > 3)
            {
                pointerY += 26;
                if (itemNum > 6)
                {
                    pointerY += 26;
                    if (itemNum > 8)
                    {
                        pointerY += 26;
                        if (itemNum > 11)
                        {
                            pointerX -= 10;
                            pointerY += 6;
                        }
                    }

                }

            }

            itemPicker.Location = new Point(pointerX, pointerY + (14 * itemNum));

            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e)
        {
            base.OnKeyboardDown(e);
            switch (e.Key)
            {
                case SdlDotNet.Input.Key.DownArrow:
                    {
                        if (itemPicker.SelectedItem == MAX_ITEMS)
                        {
                            ChangeSelected(0);
                        }
                        else
                        {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow:
                    {
                        if (itemPicker.SelectedItem == 0)
                        {
                            ChangeSelected(MAX_ITEMS);
                        }
                        else
                        {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.LeftArrow:
                    {
                        if (itemPicker.SelectedItem == 3)
                        {
                            if (tempAutoSaveSpeed > 1)
                            {
                                tempAutoSaveSpeed--;
                                ShowOption(3);
                            }
                        }
                        else if (itemPicker.SelectedItem != 12)
                        {
                            SelectItem(itemPicker.SelectedItem);
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.RightArrow:
                    {
                        if (itemPicker.SelectedItem == 3)
                        {
                            if (tempAutoSaveSpeed < 10)
                            {
                                tempAutoSaveSpeed++;
                                ShowOption(3);
                            }
                        }
                        else if (itemPicker.SelectedItem != 12)
                        {
                            SelectItem(itemPicker.SelectedItem);
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.Return:
                    {

                        SelectItem(itemPicker.SelectedItem);

                    }
                    break;
                case SdlDotNet.Input.Key.Backspace:
                    {
                        // goes to the main menu; should it?
                        WindowSwitcher.ShowMainMenu();
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum)
        {
            if (itemNum == 3)
            {
                if (tempAutoSaveSpeed < 10)
                {
                    tempAutoSaveSpeed++;
                    ShowOption(3);
                }
            }
            else if (itemNum < 12)
            {
                tempOptions[itemNum] = !tempOptions[itemNum];
                ShowOption(itemNum);
            }
            else
            {
                //Save method goes here
                IO.Options.PlayerName = tempOptions[0];
                IO.Options.PlayerDamage = tempOptions[1];
                IO.Options.PlayerBar = tempOptions[2];

                IO.Options.AutoSaveSpeed = tempAutoSaveSpeed;

                IO.Options.NpcName = tempOptions[4];
                IO.Options.NpcDamage = tempOptions[5];
                IO.Options.NpcBar = tempOptions[6];
                IO.Options.Music = tempOptions[7];
                IO.Options.Sound = tempOptions[8];
                IO.Options.SpeechBubbles = tempOptions[9];
                IO.Options.Timestamps = tempOptions[10];
                IO.Options.AutoScroll = tempOptions[11];

                WindowSwitcher.ShowMainMenu();
                this.Close();
            }

        }

        public void CreateTempOption(int itemNum)
        {
            switch (itemNum)
            {
                case 0:
                    {
                        tempOptions[itemNum] = IO.Options.PlayerName;
                    }
                    break;
                case 1:
                    {
                        tempOptions[itemNum] = IO.Options.PlayerDamage;
                    }
                    break;
                case 2:
                    {
                        tempOptions[itemNum] = IO.Options.PlayerBar;
                    }
                    break;
                case 3:
                    {
                        tempAutoSaveSpeed = IO.Options.AutoSaveSpeed;
                    }
                    break;
                case 4:
                    {
                        tempOptions[itemNum] = IO.Options.NpcName;
                    }
                    break;
                case 5:
                    {
                        tempOptions[itemNum] = IO.Options.NpcDamage;
                    }
                    break;
                case 6:
                    {
                        tempOptions[itemNum] = IO.Options.NpcBar;
                    }
                    break;
                case 7:
                    {
                        tempOptions[itemNum] = IO.Options.Music;
                    }
                    break;
                case 8:
                    {
                        tempOptions[itemNum] = IO.Options.Sound;
                    }
                    break;
                case 9:
                    {
                        tempOptions[itemNum] = IO.Options.SpeechBubbles;
                    }
                    break;
                case 10:
                    {
                        tempOptions[itemNum] = IO.Options.Timestamps;
                    }
                    break;
                case 11:
                    {
                        tempOptions[itemNum] = IO.Options.AutoScroll;
                    }
                    break;




            }
        }

        public void ShowOption(int itemNum)
        {

            switch (itemNum)
            {
                case 0:
                    {
                        lblPlayerDataName.Text = "Name: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 1:
                    {
                        lblPlayerDataDamage.Text = "Damage: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 2:
                    {
                        lblPlayerDataMiniHP.Text = "Mini-HP Bar: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 3:
                    {
                        lblPlayerDataAutoSaveSpeed.Text = "Auto-Save Speed: " + tempAutoSaveSpeed;
                    }
                    break;
                case 4:
                    {
                        lblNpcDataName.Text = "Name: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 5:
                    {
                        lblNpcDataDamage.Text = "Damage: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 6:
                    {
                        lblNpcDataMiniHP.Text = "Mini-HP Bar: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 7:
                    {
                        lblSoundDataMusic.Text = "Music: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 8:
                    {
                        lblSoundDataSound.Text = "Sound Effects: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 9:
                    {
                        lblChatDataSpeechBubbles.Text = "Speech Bubbles: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 10:
                    {
                        lblChatDataTimeStamps.Text = "TimeStamps: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 11:
                    {
                        lblChatDataAutoScroll.Text = "Auto-Scroll: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;




            }
        }

        public String BoolToString(bool setting)
        {
            if (setting == true)
            {

                return "On";
            }

            return "Off";
        }

        #endregion Methods

        public Core.WindowCore WindowPanel
        {
            get { return this; }
        }
    }
}
