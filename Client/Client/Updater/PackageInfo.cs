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
    using System.Xml;

    using PMDCP.Updater.Linker;

    public class PackageInfo : MarshalByRefObject, IPackageInfo
    {
        #region Fields

        string description;
        string fullID;
        string hash;
        string name;
        List<IInstalledPackageInfo> prerequisites;
        long size;
        int version;
        DateTime publishDate;

        #endregion Fields

        #region Constructors

        public PackageInfo() {
            prerequisites = new List<IInstalledPackageInfo>();
        }

        #endregion Constructors

        #region Properties

        public string Description {
            get { return description; }
            set { description = value; }
        }

        public string FullID {
            get { return fullID; }
            set { fullID = value; }
        }

        public string Hash {
            get { return hash; }
            set { hash = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public List<IInstalledPackageInfo> Prerequisites {
            get { return prerequisites; }
            set { prerequisites = value; }
        }

        public long Size {
            get { return size; }
            set { size = value; }
        }

        public int Version {
            get { return version; }
            set { version = value; }
        }

        public DateTime PublishDate {
            get { return publishDate; }
            set { publishDate = value; }
        }

        #endregion Properties

        #region Methods

        public void ReadBasicFromXml(XmlReader reader) {
            using (XmlReader subtreeReader = reader.ReadSubtree()) {
                while (subtreeReader.Read()) {
                    if (subtreeReader.IsStartElement()) {
                        switch (subtreeReader.Name) {
                            case "ID": {
                                    fullID = subtreeReader.ReadString();
                                }
                                break;
                            case "Version": {
                                    version = subtreeReader.ReadString().ToInt();
                                }
                                break;
                            case "Prerequisites": {
                                    ReadPrerequisites(subtreeReader);
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void ReadFullFromXml(XmlReader reader) {
            using (reader) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        switch (reader.Name) {
                            case "Name": {
                                    name = reader.ReadString();
                                }
                                break;
                            case "Description": {
                                    description = reader.ReadString();
                                }
                                break;
                            case "Size": {
                                    size = reader.ReadString().ToLong();
                                }
                                break;
                            case "Hash": {
                                    hash = reader.ReadString();
                                }
                                break;
                            case "PublishDate": {
                                    publishDate = DateTime.Parse(reader.ReadString());
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void ReadPrerequisites(XmlReader reader) {
            using (XmlReader subtreeReader = reader.ReadSubtree()) {
                while (subtreeReader.Read()) {
                    if (subtreeReader.IsStartElement()) {
                        switch (subtreeReader.Name) {
                            case "Prerequisite": {
                                    prerequisites.Add(new InstalledPackageInfo("", -1));
                                }
                                break;
                            case "ID": {
                                    prerequisites[prerequisites.Count - 1].FullID = subtreeReader.ReadString();
                                }
                                break;
                            case "Version": {
                                    prerequisites[prerequisites.Count - 1].Version = subtreeReader.ReadString().ToInt();
                                }
                                break;
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}