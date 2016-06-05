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


namespace Client.Logic.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Logic.Graphics;

    [Serializable]
    public class Tile : ICloneable
    {
        #region Properties

        public TileGraphic AnimGraphic {
            get;
            set;
        }

        public int Anim {
            get;
            set;
        }

        public int AnimSet {
            get;
            set;
        }

        public int Data1 {
            get;
            set;
        }

        public int Data2 {
            get;
            set;
        }

        public int Data3 {
            get;
            set;
        }

        public bool DoorOpen {
            get;
            set;
        }

        public TileGraphic F2AnimGraphic {
            get;
            set;
        }

        public int F2Anim {
            get;
            set;
        }

        public int F2AnimSet {
            get;
            set;
        }

        public TileGraphic FAnimGraphic {
            get;
            set;
        }

        public int FAnim {
            get;
            set;
        }

        public int FAnimSet {
            get;
            set;
        }

        public TileGraphic FringeGraphic {
            get;
            set;
        }

        public int Fringe {
            get;
            set;
        }

        public TileGraphic Fringe2Graphic {
            get;
            set;
        }

        public int Fringe2 {
            get;
            set;
        }

        public int Fringe2Set {
            get;
            set;
        }

        public int FringeSet {
            get;
            set;
        }

        public TileGraphic GroundGraphic {
            get;
            set;
        }

        public int Ground {
            get;
            set;
        }

        public int GroundSet {
            get;
            set;
        }

        public TileGraphic GroundAnimGraphic {
            get;
            set;
        }

        public int GroundAnim {
            get;
            set;
        }

        public int GroundAnimSet {
            get;
            set;
        }

        public int RDungeonMapValue {
            get;
            set;
        }

        public TileGraphic M2AnimGraphic {
            get;
            set;
        }

        public int M2Anim {
            get;
            set;
        }

        public int M2AnimSet {
            get;
            set;
        }

        public TileGraphic MaskGraphic {
            get;
            set;
        }

        public int Mask {
            get;
            set;
        }

        public TileGraphic Mask2Graphic {
            get;
            set;
        }

        public int Mask2 {
            get;
            set;
        }

        public int Mask2Set {
            get;
            set;
        }

        public int MaskSet {
            get;
            set;
        }

        public string String1 {
            get;
            set;
        }

        public string String2 {
            get;
            set;
        }

        public string String3 {
            get;
            set;
        }

        public Enums.TileType Type {
            get;
            set;
        }

        #endregion Properties

        public object Clone() {
            Tile tile = new Tile();
            tile.Ground = Ground;
            tile.GroundAnim = GroundAnim;
            tile.Mask = Mask;
            tile.Anim = Anim;
            tile.Mask2 = Mask2;
            tile.M2Anim = M2Anim;
            tile.Fringe = Fringe;
            tile.FAnim = FAnim;
            tile.Fringe2 = Fringe2;
            tile.F2Anim = F2Anim;
            tile.Type = Type;
            tile.Data1 = Data1;
            tile.Data2 = Data2;
            tile.Data3 = Data3;
            tile.String1 = String1;
            tile.String2 = String2;
            tile.String3 = String3;
            tile.RDungeonMapValue = RDungeonMapValue;
            tile.GroundSet = GroundSet;
            tile.GroundAnimSet = GroundAnimSet;
            tile.MaskSet = MaskSet;
            tile.AnimSet = AnimSet;
            tile.Mask2Set = Mask2Set;
            tile.M2AnimSet = M2AnimSet;
            tile.FringeSet = FringeSet;
            tile.FAnimSet = FAnimSet;
            tile.Fringe2Set = Fringe2Set;
            tile.F2AnimSet = F2AnimSet;
            return tile;
        }
    }
}