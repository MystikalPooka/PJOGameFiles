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
using System.Xml;
using SdlDotNet.Graphics;

namespace Client.Logic.Skins
{
    class Skin
    {
        string name;
        string creator;
        string version;
        Surface ingameBackground;

        public Surface IngameBackground {
            get { return ingameBackground; }
        }

        public string Name {
            get { return name; }
        }

        public string Creator {
            get { return creator; }
        }

        public string Version {
            get { return version; }
        }

        public void LoadSkin(string name) {
            this.name = name;

            string configPath = IO.Paths.SkinPath + name + "/Configuration/";
            if (System.IO.Directory.Exists(configPath) == false) {
                System.IO.Directory.CreateDirectory(configPath);
            }

            LoadConfigXml(configPath + "config.xml");

            ingameBackground = SkinManager.LoadGui("Game Window");
        }

        private void LoadConfigXml(string fullPath) {
            if (!IO.IO.FileExists(fullPath)) {
                SaveEmptyConfigFile(fullPath);
            }
            using (XmlReader reader = XmlReader.Create(fullPath)) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        switch (reader.Name) {
                            case "Creator": {
                                    creator = reader.ReadString();
                                }
                                break;
                            case "Version": {
                                    version = reader.ReadString();
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void SaveEmptyConfigFile(string fullPath) {
            using (XmlWriter writer = XmlWriter.Create(fullPath)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("SkinConfiguration");

                writer.WriteStartElement("General");

                writer.WriteElementString("Creator", "");
                writer.WriteElementString("Version", "");

                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void Unload() {
            if (ingameBackground != null) {
                ingameBackground.Close();
            }
        }
    }
}
