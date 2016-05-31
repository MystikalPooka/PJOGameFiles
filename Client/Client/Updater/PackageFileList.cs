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
using System.IO;
using System.Xml;

namespace PMDCP.Updater
{
    public class PackageFileList : MarshalByRefObject, IPackageFileList
    {
        List<IPackageFileListItem> items;

        public PackageFileList() {
            items = new List<IPackageFileListItem>();
        }

        public void Add(IPackageFileListItem item) {
            items.Add(item);
        }

        public int Count {
            get { return items.Count; }
        }

        public IPackageFileListItem this[int index] {
            get { return items[index]; }
        }

        public void Save(Stream stream) {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "   ";
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(stream, settings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("PackageFileList");

                for (int i = 0; i < items.Count; i++) {
                    writer.WriteStartElement("PackageFile");
                    writer.WriteElementString("File", items[i].RelativePath);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
