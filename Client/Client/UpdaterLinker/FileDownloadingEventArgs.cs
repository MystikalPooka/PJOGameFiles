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

namespace PMDCP.Updater.Linker
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class FileDownloadingEventArgs : EventArgs
    {
        #region Fields

        string fileName;
        long fileSize;
        int percent;
        long position;

        #endregion Fields

        #region Constructors

        public FileDownloadingEventArgs(long fileSize, string filePath, int percent, long position)
        {
            this.fileSize = fileSize;
            this.fileName = System.IO.Path.GetFileName(filePath).Replace(".tmp", "");
            this.percent = percent;
            this.position = position;
        }

        #endregion Constructors

        #region Properties

        public string FileName
        {
            get { return fileName; }
        }

        public long FileSize
        {
            get { return fileSize; }
        }

        public int Percent
        {
            get { return percent; }
        }

        public long Position
        {
            get { return position; }
        }

        #endregion Properties
    }
}