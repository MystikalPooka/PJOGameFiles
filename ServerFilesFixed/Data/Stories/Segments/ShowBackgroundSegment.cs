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

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class ShowBackgroundSegment : ISegment
    {
        #region Fields

        string file;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public ShowBackgroundSegment(string file) {
            Load(file);
        }

        public ShowBackgroundSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.ShowBackground; }
        }

        public string File {
            get { return file; }
            set { file = value; }
        }


        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string file) {
            this.file = file;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            this.file = parameters.GetValue("File");
        }

        public void Process(StoryState state) {
            if (Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground != null) {
                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground.Dispose();
            }
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground = Logic.Graphics.SurfaceManager.LoadSurface(IO.Paths.StoryDataPath + "Backgrounds/" + file);
        }

        #endregion Methods
    }
}