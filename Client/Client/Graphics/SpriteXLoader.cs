// This file is part of Mystery Dungeon eXtended.

// Mystery Dungeon eXtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Mystery Dungeon eXtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Mystery Dungeon eXtended.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using PMU.Compression.Zip;
using System.Xml;
using System.IO;
using System.Drawing;
using SdlDotNet.Graphics;

namespace Client.Logic.Graphics
{
    class SpriteXLoader
    {
        string path;

        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }

        public SpriteXLoader(string path)
        {
            this.path = path;

            LoadMeta();
        }

        private void LoadMeta()
        {
            using (ZipFile zipFile = ZipFile.Read(path))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    zipFile["Meta.xml"].Extract(ms);

                    ms.Seek(0, SeekOrigin.Begin);
                    using (XmlReader reader = XmlReader.Create(ms))
                    {
                        while (reader.Read())
                        {
                            if (reader.IsStartElement())
                            {
                                //switch (reader.Name) {
                                //    case "FrameWidth": {
                                //            FrameWidth = Convert.ToInt32(reader.ReadElementString());
                                //        }
                                //        break;
                                //    case "FrameHeight": {
                                //            FrameHeight = Convert.ToInt32(reader.ReadElementString());
                                //        }
                                //        break;

                                if (reader.Name == "FrameWidth")
                                {
                                    FrameWidth = Convert.ToInt32(reader.ReadElementString());
                                }
                                if (reader.Name == "FrameHeight")
                                {
                                    FrameHeight = Convert.ToInt32(reader.ReadElementString());
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<string> LoadForms()
        {
            List<string> forms = new List<string>();

            using (ZipFile zipFile = ZipFile.Read(path))
            {
                foreach (ZipEntry entry in zipFile)
                {
                    if (entry.FileName.StartsWith("Forms"))
                    {
                        string formFileName = entry.FileName.Substring(6);
                        if (!string.IsNullOrEmpty(formFileName))
                        {
                            string formName = Path.GetDirectoryName(formFileName);

                            if (forms.Contains(formName) == false)
                            {
                                forms.Add(formName);
                            }
                        }
                    }
                }
            }

            return forms;
        }

        //public string GetFrameTypeString(FrameType frameType) {
        //    switch (frameType)
        //    {
        //        case FrameType.Idle:
        //            return "Idle";
        //        case FrameType.Walk:
        //            return "Walk";
        //        case FrameType.Attack:
        //            return "Attack";
        //        case FrameType.AttackArm:
        //            return "AttackArm";
        //        case FrameType.AltAttack:
        //            return "AltAttack";
        //        case FrameType.SpAttack:
        //            return "SpAttack";
        //        case FrameType.SpAttackCharge:
        //            return "SpAttackCharge";
        //        case FrameType.SpAttackShoot:
        //            return "SpAttackShoot";
        //        case FrameType.Hurt:
        //            return "Hurt";
        //        case FrameType.Sleep:
        //            return "Sleep";
        //        default:
        //            return null;
        //    }
        //}

        //public string GetDirectionString(Enums.Direction direction) {
        //    switch (direction) {
        //        case Enums.Direction.Down:
        //            return "Down";
        //        case Enums.Direction.DownLeft:
        //            return "DownLeft";
        //        case Enums.Direction.DownRight:
        //            return "DownRight";
        //        case Enums.Direction.Left:
        //            return "Left";
        //        case Enums.Direction.Right:
        //            return "Right";
        //        case Enums.Direction.Up:
        //            return "Up";
        //        case Enums.Direction.UpLeft:
        //            return "UpLeft";
        //        case Enums.Direction.UpRight:
        //            return "UpRight";
        //        default:
        //            return null;
        //    }
        //}

        public static string GetSpriteFormString(int form, int shiny, int sex)
        {
            string formString = "r";

            if (form >= 0)
            {
                formString += "-" + form;
                if ((int)shiny >= 0)
                {
                    if (shiny == (int)Enums.Coloration.Shiny)
                    {
                        formString += "-S";
                    }
                    else if (shiny == (int)Enums.Coloration.Normal)
                    {
                        formString += "-N";
                    }
                    if ((int)sex >= 0)
                    {
                        if (sex == (int)Enums.Sex.Female)
                        {
                            formString += "-F";
                        }
                        else if (sex == (int)Enums.Sex.Male)
                        {
                            formString += "-M";
                        }
                        else if (sex == (int)Enums.Sex.Genderless)
                        {
                            formString += "-G";
                        }
                    }
                }
            }
            return formString;
        }

        public void LoadSpriteFromFile(string filename)
        {
            //Not implemented
            //TODO: Load single sprite images

            Bitmap bitmap = (Bitmap)Image.FromFile(filename);
            Surface sheetSurface = new Surface(bitmap);
            sheetSurface.Transparent = true;
        }

        ////Duplicate code!
        public void Load(SpriteSheet sheet, FrameData frameData, string overrideForm)
        {
            string formDirectory = "Forms/" + overrideForm + "/";

            frameData.SetFrameSize(FrameWidth, FrameHeight);
            using (ZipFile zipFile = ZipFile.Read(path))
            {
                foreach (FrameType frameType in Enum.GetValues(typeof(FrameType))) {
                    if (FrameTypeHelper.IsFrameTypeDirectionless(frameType) == false) {
                        for (int i = 0; i < 8; i++) {
                            Enums.Direction dir = GraphicsManager.GetAnimIntDir(i);

                            using (MemoryStream ms = new MemoryStream()) {
                                //string fullImageString = formDirectory + GetFrameTypeString(frameType) + "-" + GetDirectionString(dir) + ".png";
                                string fullImageString = formDirectory + frameType.ToString() + "-" + dir.ToString() + ".png";

                                if (zipFile.ContainsEntry(fullImageString)) {
                                    zipFile[fullImageString].Extract(ms);

                                    ms.Seek(0, SeekOrigin.Begin);
                                    Bitmap bitmap = (Bitmap)Image.FromStream(ms);
                                    Surface sheetSurface = new Surface(bitmap);
                                    sheetSurface.Transparent = true;

                                    //still loads from these
                                    sheet.AddSheet(frameType, dir, sheetSurface);

                                    frameData.SetFrameCount(frameType, dir, bitmap.Width / frameData.FrameWidth);
                                } else {
                                    frameData.SetFrameCount(frameType, dir, 0);
                                }
                            }
                        }
                    } else {
                        using (MemoryStream ms = new MemoryStream()) {
                            //string fullImageString = formDirectory + GetFrameTypeString(frameType) + "-" + GetDirectionString(Enums.Direction.Down) + ".png";
                            string fullImageString = formDirectory + frameType.ToString() + "-Down.png";

                            if (zipFile.ContainsEntry(fullImageString)) {
                                zipFile[fullImageString].Extract(ms);

                                ms.Seek(0, SeekOrigin.Begin);
                                Bitmap bitmap = (Bitmap)Image.FromStream(ms);
                                Surface sheetSurface = new Surface(bitmap);
                                sheetSurface.Transparent = true;

                                sheet.AddSheet(frameType, Enums.Direction.Down, sheetSurface);

                                frameData.SetFrameCount(frameType, Enums.Direction.Down, bitmap.Width / frameData.FrameWidth);
                            } else {
                                frameData.SetFrameCount(frameType, Enums.Direction.Down, 0);
                            }
                        }
                    }
                }
            }
        }
    }
}