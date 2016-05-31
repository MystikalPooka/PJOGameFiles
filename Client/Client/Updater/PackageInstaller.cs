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
using Ionic.Zip;
using System.IO;

namespace PMDCP.Updater
{
    public class PackageInstaller : MarshalByRefObject, IPackageInstaller
    {
        string packagePath;
        string basePath;
        ZipFile packageZip;
        IPackageInfo package;
        string packageDataDirectory;
        IPackageLoader loader;

        public PackageInstaller(string packagePath, string packageDataDirectory, string basePath, IPackageInfo package, IPackageLoader loader) {
            this.packagePath = packagePath;
            this.packageDataDirectory = packageDataDirectory;
            this.basePath = basePath;
            this.package = package;
            this.loader = loader;
            packageZip = new ZipFile(packagePath);
        }

        public void ExtractFile(string file, string destinationPath) {
            foreach (ZipEntry entry in packageZip) {
                if (entry.FileName == "Files/" + file) {
                    //entry.FileName = destinationPath;
                    string fullPath = basePath + destinationPath;
                    if (!Directory.Exists(Path.GetDirectoryName(fullPath))) {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                    }
                    //if (IsFileLocked(fullPath, false)) {
                    //    if (FileExists(fullPath + ".ToDelete", false)) {
                    //        DeleteFile(fullPath + ".ToDelete", false);
                    //    }
                    //    File.Move(fullPath, fullPath + ".ToDelete");
                    //}
                    using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                        entry.Extract(stream);
                    }
                    return;
                }
            }
        }

        public void DeleteFile(string filePath, bool relative) {
            string fullPath;
            if (relative) {
                fullPath = basePath + filePath;
            } else {
                fullPath = filePath;
            }
            if (File.Exists(fullPath)) {
                File.Delete(fullPath);
            }
        }

        public bool FileExists(string filePath, bool relative) {
            string fullPath;
            if (relative) {
                fullPath = basePath + filePath;
            } else {
                fullPath = filePath;
            }
            return File.Exists(fullPath);
        }

        public string GetFullPath(string filePath) {
            return basePath + filePath;
        }

        public string PackagePath {
            get { return packagePath; }
        }

        public void ExtractFile(string file) {
            ExtractFile(file, file);
        }

        public void ExtractAll() {
            foreach (ZipEntry entry in packageZip) {
                if (entry.FileName.StartsWith("Files/") && entry.FileName != "Files/") {
                    //entry.FileName = entry.FileName.Remove(0, 6);
                    string fullPath = basePath + entry.FileName.Remove(0, 6);
                    if (!Directory.Exists(Path.GetDirectoryName(fullPath))) {
                        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                    }
                    //if (IsFileLocked(fullPath, false)) {
                    //    if (FileExists(fullPath + ".ToDelete", false)) {
                    //        DeleteFile(fullPath + ".ToDelete", false);
                    //    }
                    //    File.Move(fullPath, fullPath + ".ToDelete");
                    //}
                    using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                        entry.Extract(stream);
                    }
                }
            }
        }

        public void Close() {
            packageZip.Dispose();
        }

        public bool IsFileLocked(string filePath, bool relative) {
            if (relative) {
                filePath = GetFullPath(filePath);
            }

            FileStream stream = null;

            try {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            } catch (IOException) {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            } finally {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public string PackageDataDirectory {
            get { return packageDataDirectory; }
        }

        public IPackageInfo Package {
            get { return package; }
        }

        public IPackageLoader Loader {
            get { return loader; }
        }
    }
}
