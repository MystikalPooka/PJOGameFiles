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


namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class WarpSegment : ISegment
    {
        #region Fields

        int x;
        int y;
        StoryState storyState;
        string map;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public WarpSegment(string map, int x, int y) {
            Load(map, x, y);
        }

        public WarpSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.Warp; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public string Map {
            get { return map; }
            set { map = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string map, int x, int y) {
            this.map = map;
            this.x = x;
            this.y = y;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("MapID"), parameters.GetValue("X").ToInt(), parameters.GetValue("Y").ToInt());
        }

        public void Process(StoryState state) {
            Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("actonaction"));

            state.StoryPaused = true;
            state.Pause();
        }

        #endregion Methods
    }
}