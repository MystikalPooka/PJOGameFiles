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
    class Snow : IWeather
    {
        #region Fields

        bool disposed;

        List<Snowflake> snowflakes = new List<Snowflake>();

        #endregion Fields

        #region Constructors

        public Snow() {
            disposed = false;

            for (int i = 0; i < 50; i++) {
                snowflakes.Add(new Snowflake());
            }
        }

        #endregion Constructors

        #region Properties

        public bool Disposed {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources() {
            disposed = true;
            for (int i = snowflakes.Count - 1; i >= 0; i--) {
                snowflakes[i].Dispose();
                snowflakes.RemoveAt(i);
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            for (int i = 0; i < snowflakes.Count; i++) {
                snowflakes[i].UpdateLocation(1);
                destData.Blit(snowflakes[i], new Point(snowflakes[i].X, snowflakes[i].Y));
            }
        }

        #endregion Methods

        public Enums.Weather ID {
            get { return Enums.Weather.Snowing; }
        }
    }
}
