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


/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 28/09/2009
 * Time: 4:06 PM
 * 
 */
using System;
using System.Xml;
using PMU.Core;
using SdlInput = SdlDotNet.Input;

namespace Client.Logic.IO
{
    /// <summary>
    /// Description of ControlLoader.
    /// </summary>
    internal class ControlLoader
    {
        #region Properties

        public static SdlInput.Key AcceptKey {
            get;
            set;
        }

        public static SdlInput.Key AttackKey {
            get;
            set;
        }

        public static SdlInput.Key DownKey {
            get;
            set;
        }

        public static SdlInput.Key HealKey {
            get;
            set;
        }

        public static SdlInput.Key LeaderSwitchKey {
            get;
            set;
        }

        public static SdlInput.Key LeftKey {
            get;
            set;
        }
        
        public static SdlInput.Key TurnKey {
            get;
            set;
        }

        public static SdlInput.Key Move1Key {
            get;
            set;
        }

        public static SdlInput.Key Move2Key {
            get;
            set;
        }

        public static SdlInput.Key Move3Key {
            get;
            set;
        }

        public static SdlInput.Key Move4Key {
            get;
            set;
        }

        public static SdlInput.Key RightKey {
            get;
            set;
        }

        public static SdlInput.Key RunKey {
            get;
            set;
        }

        public static SdlInput.Key Slot1SwitchKey {
            get;
            set;
        }

        public static SdlInput.Key Slot2SwitchKey {
            get;
            set;
        }

        public static SdlInput.Key Slot3SwitchKey {
            get;
            set;
        }

        public static SdlInput.Key SwitchModeKey {
            get;
            set;
        }

        public static SdlInput.Key TargetSelectAcceptKey {
            get;
            set;
        }

        public static SdlInput.Key TargetSelectCancelKey {
            get;
            set;
        }

        public static SdlInput.Key TargetSelectLeftKey {
            get;
            set;
        }

        public static SdlInput.Key TargetSelectRightKey {
            get;
            set;
        }

        public static SdlInput.Key UpKey {
            get;
            set;
        }

        #endregion Properties

        public static void InitDefaultControls() {
            UpKey = SdlInput.Key.UpArrow;
            DownKey = SdlInput.Key.DownArrow;
            LeftKey = SdlInput.Key.LeftArrow;
            RightKey = SdlInput.Key.RightArrow;
            TurnKey = SdlInput.Key.Home;
            RunKey = SdlInput.Key.LeftShift;
            AttackKey = SdlInput.Key.F;
        }

        public static void LoadControls() {
            InitDefaultControls();
            if (IO.FileExists("controls.xml") == false) {
                SaveControls();
            }
            using (XmlReader reader = XmlReader.Create(Paths.StartupPath + "controls.xml")) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        switch (reader.Name) {
                            case "Up": {
                                    UpKey = (SdlInput.Key)reader.ReadString().ToInt((int)SdlInput.Key.UpArrow);
                                }
                                break;
                            case "Down": {
                                    DownKey = (SdlInput.Key)reader.ReadString().ToInt((int)SdlInput.Key.DownArrow);
                                }
                                break;
                            case "Left": {
                                    LeftKey = (SdlInput.Key)reader.ReadString().ToInt((int)SdlInput.Key.LeftArrow);
                                }
                                break;
                            case "Right": {
                                    RightKey = (SdlInput.Key)reader.ReadString().ToInt((int)SdlInput.Key.RightArrow);
                                }
                                break;
                            case "Turn": {
                                    TurnKey = (SdlInput.Key)reader.ReadString().ToInt((int)SdlInput.Key.Home);
                                }
                                break;
                            case "Run": {
                                    RunKey = (SdlInput.Key)reader.ReadString().ToInt((int)SdlInput.Key.LeftShift);
                                }
                                break;
                            //case "StandardAttack": {
                            //        AttackKey = (SdlInput.Key)reader.ReadString().ToInt((int)SdlInput.Key.Z);
                            //    }
                            //    break;
                        }
                    }
                }
            }
        }

        public static void SaveControls() {
            using (XmlWriter writer = XmlWriter.Create(Paths.StartupPath + "controls.xml", Options.XmlWriterSettings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("Controls");

                writer.WriteStartElement("Movement");

                writer.WriteElementString("Up", ((int)UpKey).ToString());
                writer.WriteElementString("Down", ((int)DownKey).ToString());
                writer.WriteElementString("Left", ((int)LeftKey).ToString());
                writer.WriteElementString("Right", ((int)RightKey).ToString());
                writer.WriteElementString("Turn", ((int)TurnKey).ToString());
                writer.WriteElementString("Run", ((int)RunKey).ToString());

                writer.WriteEndElement();
                writer.WriteStartElement("Attacking");

                writer.WriteElementString("StandardAttack", ((int)AttackKey).ToString());

                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
