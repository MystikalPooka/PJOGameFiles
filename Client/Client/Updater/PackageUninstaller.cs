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

using System;
using System.Collections.Generic;
using System.Text;

using PMDCP.Updater.Linker;

namespace PMDCP.Updater
{
    public class PackageUninstaller : MarshalByRefObject, IPackageUninstaller
    {
        IPackageInfo packageInfo;
        IPackageFileList packageFileList;

        public IPackageInfo Package {
            get { return packageInfo; }
        }

        public IPackageFileList FileList {
            get { return packageFileList; }
        }

        public PackageUninstaller(IPackageInfo packageInfo, IPackageFileList packageFileList) {
            this.packageInfo = packageInfo;
            this.packageFileList = packageFileList;
        }

        public void DeleteFile(string filePath, bool relative) {

        }

        public void DeleteFile(IPackageFileListItem packageFile) {
            System.IO.File.Delete(packageFile.FullPath);
        }

        public void BasicUninstall() {
            for (int i = 0; i < packageFileList.Count; i++) {
                DeleteFile(packageFileList[i]);
            }
        }
    }
}
