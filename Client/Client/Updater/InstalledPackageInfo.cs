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
    public class InstalledPackageInfo : MarshalByRefObject, IInstalledPackageInfo
    {
        public int Version { get; set; }
        public string FullID { get; set; }

        public InstalledPackageInfo(string fullID, int version) {
            FullID = fullID;
            Version = version;
        }

        public bool Match(string fullID, int version) {
            if (version != -1) {
                return (fullID == FullID && version == Version);
            } else {
                return (fullID == FullID);
            }
        }

        public bool MatchIfNewer(string fullID, int versionToTest) {
            if (versionToTest != -1) {
                return (fullID == FullID && versionToTest <= Version);
            } else {
                return (fullID == FullID);
            }
        }
    }
}
