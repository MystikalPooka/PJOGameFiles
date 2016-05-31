using PMU.Compression.Zip;
using SdlDotNet.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace Client.Logic.Graphics
{
    /// <summary>
    /// SpriteXLoader written by Pikablu for Mystery Dungeon eXtended (MDX) (http://pmdcommunity.x10.mx). This code has been adopted for Pokemon Journey Online (by Draco) with permission. This code, and all derivations, may not be used in other projects without expression permission from Pikablu at MDX.
    /// 
    /// This header should remain in all derivations of this code.
    /// </summary>
    class SpriteXLoader
    {
        string path;

        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }

        public SpriteXLoader(string path) {
            this.path = path;

            LoadMeta();
        }

        private void LoadMeta() {
            using (ZipFile zipFile = ZipFile.Read(path)) {
                using (MemoryStream ms = new MemoryStream()) {
                    zipFile["Meta.xml"].Extract(ms);

                    ms.Seek(0, SeekOrigin.Begin);
                    using (XmlReader reader = XmlReader.Create(ms)) {
                        while (reader.Read()) {
                            if (reader.IsStartElement()) {
                                switch (reader.Name) {
                                    case "FrameWidth": {
                                            FrameWidth = Convert.ToInt32(reader.ReadElementString());
                                        }
                                        break;
                                    case "FrameHeight": {
                                            FrameHeight = Convert.ToInt32(reader.ReadElementString());
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<string> LoadForms() {
            List<string> forms = new List<string>();

            using (ZipFile zipFile = ZipFile.Read(path)) {
                foreach (ZipEntry entry in zipFile) {
                    if (entry.FileName.StartsWith("Forms")) {
                        string formName = Path.GetDirectoryName(entry.FileName.Substring(6));

                        if (forms.Contains(formName) == false) {
                            forms.Add(formName);
                        }
                    }
                }
            }

            return forms;
        }

        public string GetFrameTypeString(FrameType frameType) {
            switch (frameType) {
                case FrameType.Idle:
                    return "Idle";
                case FrameType.Walk:
                    return "Walk";
                case FrameType.Attack:
                    return "Attack";
                case FrameType.AttackArm:
                    return "AttackArm";
                case FrameType.AltAttack:
                    return "AltAttack";
                case FrameType.SpAttack:
                    return "SpAttack";
                case FrameType.SpAttackCharge:
                    return "SpAttackCharge";
                case FrameType.SpAttackShoot:
                    return "SpAttackShoot";
                case FrameType.Hurt:
                    return "Hurt";
                case FrameType.Sleep:
                    return "Sleep";
                default:
                    return null;
            }
        }

        public string GetDirectionString(Enums.Direction direction) {
            switch (direction) {
                case Enums.Direction.Down:
                    return "Down";
                case Enums.Direction.DownLeft:
                    return "DownLeft";
                case Enums.Direction.DownRight:
                    return "DownRight";
                case Enums.Direction.Left:
                    return "Left";
                case Enums.Direction.Right:
                    return "Right";
                case Enums.Direction.Up:
                    return "Up";
                case Enums.Direction.UpLeft:
                    return "UpLeft";
                case Enums.Direction.UpRight:
                    return "UpRight";
                default:
                    return null;
            }
        }

        public static string GetSpriteFormString(int form, int shiny, int sex) {
            string formString = "r";

            if (form >= 0) {
                formString += "-" + form;
                if ((int)shiny >= 0) {
                    if (shiny == (int)Enums.Coloration.Shiny) {
                        formString += "-S";
                    } else if (shiny == (int)Enums.Coloration.Normal) {
                        formString += "-N";
                    }
                    if ((int)sex >= 0) {
                        if (sex == (int)Enums.Sex.Female) {
                            formString += "-F";
                        } else if (sex == (int)Enums.Sex.Male) {
                            formString += "-M";
                        } else if (sex == (int)Enums.Sex.Genderless) {
                            formString += "-G";
                        }
                    }
                }
            }
            return formString;
        }

        public void Load(SpriteSheet sheet, FrameData frameData, string overrideForm) {
            string formDirectory = "Forms/" + overrideForm + "/";

            frameData.SetFrameSize(FrameWidth, FrameHeight);
            using (ZipFile zipFile = ZipFile.Read(path)) {

                foreach (FrameType frameType in Enum.GetValues(typeof(FrameType))) {
                    if (FrameTypeHelper.IsFrameTypeDirectionless(frameType) == false) {
                        for (int i = 0; i < 8; i++) {
                            Enums.Direction dir = GraphicsManager.GetAnimIntDir(i);

                            using (MemoryStream ms = new MemoryStream()) {
                                string fullImageString = formDirectory + GetFrameTypeString(frameType) + "-" + GetDirectionString(dir) + ".png";

                                if (zipFile.ContainsEntry(fullImageString)) {
                                    zipFile[fullImageString].Extract(ms);

                                    ms.Seek(0, SeekOrigin.Begin);
                                    Bitmap bitmap = (Bitmap)Image.FromStream(ms);
                                    Surface sheetSurface = new Surface(bitmap);
                                    sheetSurface.Transparent = true;

                                    sheet.AddSheet(frameType, dir, sheetSurface);

                                    frameData.SetFrameCount(frameType, dir, bitmap.Width / frameData.FrameWidth);
                                } else {
                                    frameData.SetFrameCount(frameType, dir, 0);
                                }
                            }
                        }
                    } else {
                        using (MemoryStream ms = new MemoryStream()) {
                            string fullImageString = formDirectory + GetFrameTypeString(frameType) + "-" + GetDirectionString(Enums.Direction.Down) + ".png";

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
