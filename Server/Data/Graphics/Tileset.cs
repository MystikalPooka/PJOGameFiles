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
using System.Drawing;
using System.IO;
using PMU.Core;
using SdlDotNet.Graphics;

namespace Client.Logic.Graphics
{
    class Tileset : LRUCache<int, TileGraphic>
    {
        int tileCount;
        int tileSetNumber;
        Size size;
        string filePath;
        Object lockObject = new object();

        long headerSize;
        long[] tilePositionCache;
        int[] tileSizeCache;

        public Size Size {
            get { return this.size; }
        }

        public int TilesetNumber {
            get { return tileSetNumber; }
        }

        public int TileCount {
            get { return tileCount; }
        }

        public Tileset(int tileSetNumber, int maxCacheSize)
            : base(maxCacheSize) {
            this.tileSetNumber = tileSetNumber;
            size = new Size();
        }

        public long GetTilePosition(int index)
        {
            return tilePositionCache[index] + headerSize;
        }

        public void Load(string filePath) {
            this.filePath = filePath;
            // File format:
            // [tileset-width(4)][tileset-height(4)][tile-count(4)]
            // [tileposition-1(4)][tilesize-1(4)][tileposition-2(4)][tilesize-2(4)][tileposition-n(n*4)][tilesize-n(n*4)]
            // [tile-1(variable)][tile-2(variable)][tile-n(variable)]
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    // Read tileset width
                    this.size.Width = reader.ReadInt32();
                    // Read tileset height
                    this.size.Height = reader.ReadInt32();

                    this.tileCount = (size.Width / Constants.TILE_WIDTH) * (size.Height / Constants.TILE_HEIGHT);

                    // Prepare tile information cache
                    this.tilePositionCache = new long[tileCount];
                    this.tileSizeCache = new int[tileCount];

                    // Load tile information
                    for (int i = 0; i < tileCount; i++)
                    {
                        // Read tile position data
                        this.tilePositionCache[i] = reader.ReadInt64();
                        // Read tile size data
                        this.tileSizeCache[i] = reader.ReadInt32();
                    }
                    headerSize = stream.Position;
                }
            }
            SetupInitialDataFromTile(0);
        }

        private void SetupInitialDataFromTile(int tileNumber) {

            byte[] tileBytes = new byte[tileSizeCache[tileNumber]];
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Seek to the location of the tile
                stream.Seek(GetTilePosition(tileNumber), SeekOrigin.Begin);
                stream.Read(tileBytes, 0, tileBytes.Length);
            }
            using (MemoryStream stream = new MemoryStream(tileBytes))
            {
                Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                Surface tileSurface = new Surface(bitmap);

                tileSurface.Transparent = true;
                int rawSurfaceSize = tileSurface.Width * tileSurface.Height * tileSurface.BitsPerPixel / 8;
                TileGraphic tileGraphic = new TileGraphic(this.tileSetNumber, tileNumber, tileSurface, rawSurfaceSize);
                //base.Add(tileNumber, tileGraphic);
                base.Add(tileNumber, tileGraphic);
            }

        }

        public TileGraphic GetTileGraphic(int tileNumber) {
            if (tileNumber > -1 && tileNumber < tileCount) {

                TileGraphic graphic = base.Get(tileNumber);
                if (graphic != null) {
                    return graphic;
                } else {
                    byte[] tileBytes = new byte[this.tileSizeCache[tileNumber]];
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                        // Seek to the location of the tile
                        stream.Seek(GetTilePosition(tileNumber), SeekOrigin.Begin);
                        stream.Read(tileBytes, 0, tileBytes.Length);
                    }
                    using (MemoryStream stream = new MemoryStream(tileBytes))
                    {
                        Bitmap bitmap = (Bitmap)Image.FromStream(stream);
                        Surface tileSurface = new Surface(bitmap);
                        tileSurface.Transparent = true;
                        int rawSurfaceSize = tileSurface.Width * tileSurface.Height * tileSurface.BitsPerPixel / 8;
                        TileGraphic tileGraphic = new TileGraphic(this.tileSetNumber, tileNumber, tileSurface, rawSurfaceSize);
                        //base.Add(tileNumber, tileGraphic);
                        base.Add(tileNumber, tileGraphic);
                        //tiles[tileNumber] = tileGraphic;
                        return tileGraphic;
                    }
                }

            } else {
                return GetTileGraphic(0);
            }
        }

        public Surface this[int tileNumber] {
            get { return GetTileGraphic(tileNumber).Tile; }
        }
    }
}
