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

namespace PMDCP.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMDCP.Updater.Linker;

    public class UpdateCheckResult : MarshalByRefObject, IUpdateCheckResult
    {
        #region Fields

        string updateURI;
        string updateDirectory;
        List<IPackageInfo> packagesToUpdate;

        #endregion Fields

        #region Constructors

        public UpdateCheckResult(string updateURI, string updateDirectory) {
            this.updateURI = updateURI;
            this.updateDirectory = updateDirectory;
            packagesToUpdate = new List<IPackageInfo>();
        }

        #endregion Constructors

        #region Properties

        public string UpdateURI {
            get { return updateURI; }
        }

        #endregion Properties

        public List<IPackageInfo> PackagesToUpdate {
            get { return packagesToUpdate; }
            set { packagesToUpdate = value; }
        }

        public string UpdateDirectory {
            get { return updateDirectory; }
        }
    }
}