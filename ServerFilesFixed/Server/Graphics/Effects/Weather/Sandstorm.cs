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

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;
using SdlDotNet.Graphics.Sprites;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Sandstorm : IWeather
    {
        #region Fields

        Surface buffer;
        bool disposed;
        int X;

        #endregion Fields

        #region Constructors

        public Sandstorm()
        {
            disposed = false;
            buffer = new Surface(24 * Constants.TILE_WIDTH, 15 * Constants.TILE_HEIGHT);
            for (int x = 0; x < 24; x++)
            {
                for (int y = 0; y < 15; y++)
                {

                    buffer.Blit(GraphicsManager.Tiles[10][76 + 14 * (x % 4)], new Point(x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT));
                }
            }
            buffer.AlphaBlending = true;
            buffer.Alpha = 160;
            X = 0;
        }

        #endregion Constructors

        #region Properties

        public SdlDotNet.Graphics.Surface Buffer
        {
            get { return buffer; }
        }

        public bool Disposed
        {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources()
        {
            disposed = true;
            buffer.Dispose();
        }

        public void Render(Renderers.RendererDestinationData destData, int tick)
        {
            // We don't need to render anything as this overlay isn't animated and always remains the same
            X = (X + 4) % 128;
            destData.Blit(buffer, new Point((-128 + X), 0));
        }

        #endregion Methods

        public Enums.Weather ID
        {
            get { return Enums.Weather.Sandstorm; }
        }
    }
}
