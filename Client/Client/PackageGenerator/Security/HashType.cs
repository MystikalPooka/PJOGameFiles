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
using System.ComponentModel;

namespace PMDCP.Updater.PackageGenerator.Security
{
    public enum HashType
    {
        [Description("SHA-1")]
        SHA1,
        [Description("SHA-256")]
        SHA256,
        [Description("SHA-384")]
        SHA384,
        [Description("SHA-512")]
        SHA512,
        [Description("MD5")]
        MD5,
        [Description("RIPEMD-160")]
        RIPEMD160
    }
}
