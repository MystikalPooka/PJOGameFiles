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


namespace Client.Logic.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using PMU.Core;

    using SdlDotNet.Graphics;
    using System.IO;

    #region Enumerations

    public enum FrameType
    {
        Idle = 0,
        Walk,
        Attack,
        AttackArm,
        AltAttack,
        SpAttack,
        SpAttackCharge,
        SpAttackShoot,
        Hurt,
        Sleep
    }

    #endregion Enumerations

    class FrameTypeHelper
    {
        public static bool IsFrameTypeDirectionless(FrameType frameType) {
            return frameType == FrameType.Sleep ? true : false;
        }
    }

    class FrameData
    {
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }

        Dictionary<FrameType, Dictionary<Enums.Direction, int>> frameCount;

        public FrameData() {
            frameCount = new Dictionary<FrameType, Dictionary<Enums.Direction, int>>();
        }

        #region Methods
        public void SetFrameSize(int frameWidth, int frameHeight)
        {
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
        }
        public void SetFrameSize(int animWidth, int animHeight, int frames) {
            FrameWidth = animWidth / frames;

            FrameHeight = animHeight;
        }

        public void SetFrameCount(FrameType type, Enums.Direction dir, int count) {
            if (frameCount.ContainsKey(type) == false) {
                frameCount.Add(type, new Dictionary<Enums.Direction, int>());
            }
            if (frameCount[type].ContainsKey(dir) == false) {
                frameCount[type].Add(dir, count);
            } else {
                frameCount[type][dir] = count;
            }
        }

        public int GetFrameCount(FrameType type, Enums.Direction dir) {
            Dictionary<Enums.Direction, int> dirs = null;
            if (frameCount.TryGetValue(type, out dirs)) {
                int value = 0;
                if (dirs.TryGetValue(dir, out value)) {
                    return value;
                } else {
                    return 0;
                }
            } else {
                return 0;
            }
        }

        #endregion Methods
    }

    class SpriteSheet : ICacheable
    {
        #region Fields

        FrameData frameData;
        int num;
        Surface sheet;
        int sizeInBytes;
        string form;

        #endregion Fields

        public SpriteSheet(int num, string form) {
            this.num = num;
            this.form = form;

            frameData = new FrameData();
        }

        #region Properties

        public int BytesUsed {
            get { return sizeInBytes; }
        }

        public FrameData FrameData {
            get { return frameData; }
        }

        public int Num {
            get { return num; }
        }

        public string Form {
            get { return form; }
        }

        Dictionary<FrameType, Dictionary<Enums.Direction, Surface>> animations;

        #endregion Properties

        #region Methods


        public Rectangle GetFrameBounds(FrameType frameType, Enums.Direction direction, int frameNum) {
            Rectangle rec = new Rectangle();
            rec.X = frameNum * frameData.FrameWidth;
            rec.Y = 0;
            rec.Width = frameData.FrameWidth;
            rec.Height = frameData.FrameHeight;

            return rec;
        }

        public void LoadFromData(BinaryReader reader, int totalByteSize)
        {
            frameData = new FrameData();
            animations = new Dictionary<FrameType, Dictionary<Enums.Direction, Surface>>();
            
            foreach (FrameType frameType in Enum.GetValues(typeof(FrameType))) {
                if (FrameTypeHelper.IsFrameTypeDirectionless(frameType) == false) {
                    for (int i = 0; i < 8; i++)
                    {
                        Enums.Direction dir = GraphicsManager.GetAnimIntDir(i);
                        int frameCount = reader.ReadInt32();
                        frameData.SetFrameCount(frameType, dir, frameCount);
                        int size = reader.ReadInt32();
                        if (size > 0)
                        {
                            byte[] imgData = reader.ReadBytes(size);
                            using (MemoryStream stream = new MemoryStream(imgData))
                            {
                                Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                                Surface sheetSurface = new Surface(bitmap);
                                sheetSurface.Transparent = true;

                                AddSheet(frameType, dir, sheetSurface);

                                frameData.SetFrameSize(sheetSurface.Width, sheetSurface.Height, frameCount);
                            }
                        }
                    }
                } else {
                    int frameCount = reader.ReadInt32();
                    frameData.SetFrameCount(frameType, Enums.Direction.Down, frameCount);
                    int size = reader.ReadInt32();
                    if (size > 0)
                    {
                        byte[] imgData = reader.ReadBytes(size);

                        using (MemoryStream stream = new MemoryStream(imgData))
                        {

                            Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                            Surface sheetSurface = new Surface(bitmap);
                            sheetSurface.Transparent = true;

                            AddSheet(frameType, Enums.Direction.Down, sheetSurface);

                            frameData.SetFrameSize(sheetSurface.Width, sheetSurface.Height, frameCount);
                        }
                    }
                }
            }
            this.sizeInBytes = totalByteSize;
        }
        public void LoadSpriteX(SpriteXLoader loader, string overrideForm)
        {
            frameData = new FrameData();
            animations = new Dictionary<FrameType, Dictionary<Enums.Direction, Surface>>();

            loader.Load(this, frameData, overrideForm);

            this.sizeInBytes = 5 * 1000;
        }

        public Surface GetSheet(FrameType type, Enums.Direction dir) {
            if (FrameTypeHelper.IsFrameTypeDirectionless(type)) {
                dir = Enums.Direction.Down;
            }
            return animations[type][dir];
        }

        public void AddSheet(FrameType type, Enums.Direction dir, Surface surface) {
            if (!animations.ContainsKey(type)) {
                animations.Add(type, new Dictionary<Enums.Direction, Surface>());
            }
            if (animations[type].ContainsKey(dir) == false) {
                animations[type].Add(dir, surface);
            } else {
                animations[type][dir] = surface;
            }
        }

        #endregion Methods
    }
}