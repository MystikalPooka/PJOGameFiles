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
    using System.Threading;
    using System.IO;
    using Ionic.Zip;

    public class UpdateClient : MarshalByRefObject, IUpdateClient
    {
        #region Fields

        List<InstalledPackageInfo> installedPackages;
        string packageListDirectory;
        Security.Encryption packageListEncryption;
        string status;

        public string Status {
            get { return status; }
            set { status = value; }
        }
        public event EventHandler InstallationComplete;
        public event EventHandler<PackageDownloadStartEventArgs> PackageDownloadStart;
        public event EventHandler StatusUpdated;
        public event EventHandler<PackageInstallationCompleteEventArgs> PackageInstallationComplete;

        #endregion Fields

#if DEBUG
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;
#endif

        #region Constructors

        public UpdateClient(string packageListKey, string packageListDirectory) {
#if DEBUG
            AttachConsole(ATTACH_PARENT_PROCESS);
#endif

            this.packageListEncryption = new Security.Encryption(packageListKey);
            this.packageListDirectory = packageListDirectory;
            installedPackages = new List<InstalledPackageInfo>();
        }

        #endregion Constructors

        #region Methods

        public IUpdateCheckResult CheckForUpdates(string updateURI) {
            UpdateCheckResult result = new UpdateCheckResult(updateURI, Substring(updateURI, 0, updateURI.LastIndexOf("/") + 1));

            List<IPackageInfo> allPackages = ReadUpdateFile(result);
            for (int i = 0; i < allPackages.Count; i++) {
                for (int reqs = 0; reqs < allPackages[i].Prerequisites.Count; reqs++) {
                    if (allPackages[i].Prerequisites[reqs].Version == -1) {
                        IPackageInfo packageInfo = FindPackageByID(allPackages, allPackages[i].Prerequisites[reqs].FullID);
                        if (packageInfo != null) {
                            allPackages[i].Prerequisites[reqs].Version = packageInfo.Version;
                        }
                    }
                }
                if (CanInstallPackage(allPackages[i])) {
                    // We need to install this package. So, let's read the rest of the data
                    ((PackageInfo)allPackages[i]).ReadFullFromXml(XmlReader.Create(result.UpdateDirectory + "Packages/" + allPackages[i].FullID + "/" + allPackages[i].FullID + ".xml"));
                    // And add it to the list of packages in the update
                    result.PackagesToUpdate.Add(allPackages[i]);
                }
            }

            return result;
        }

        private IPackageInfo FindPackageByID(List<IPackageInfo> packageList, string id) {
            for (int i = 0; i < packageList.Count; i++) {
                if (packageList[i].FullID == id) {
                    return packageList[i];
                }
            }
            return null;
        }

        public IFileDownload CreateFileDownload(string downloadURI, string filePath) {
            return new FileDownload(downloadURI, filePath);
        }

        public void PerformUpdate(IUpdateCheckResult result) {
            if (result.PackagesToUpdate.Count > 0) {
                DownloadPackage(result, 0);
            }
        }

        private void DownloadPackage(IUpdateCheckResult result, int currentPackage) {
            IPackageInfo package = result.PackagesToUpdate[currentPackage];
            string updaterCache = this.packageListDirectory + "UpdaterCache/";
            if (Directory.Exists(updaterCache) == false) {
                Directory.CreateDirectory(updaterCache);
            }
            string tempPath = updaterCache + package.FullID + ".uprtmp";//Path.GetTempFileName();
            FileDownload download = new FileDownload(result.UpdateDirectory + "Packages/" + package.FullID + "/" + package.FullID + ".zip", tempPath);
            if (PackageDownloadStart != null) {
                PackageDownloadStart(this, new PackageDownloadStartEventArgs(package, download));
            }
            download.DownloadUpdate += delegate(System.Object o, FileDownloadingEventArgs e)
            {
                //if (UpdateDownloading != null) {
                //    UpdateDownloading(this, new UpdateInstallingEventArgs(e, fileNum, fileList.Count));
                //}
            };
            download.DownloadComplete += delegate(System.Object o, FileDownloadingEventArgs e)
            {
                InstallDownloadedPackage(result, currentPackage, tempPath);
            };
            download.Download();
        }

        private void InstallDownloadedPackage(IUpdateCheckResult result, int currentPackage, string packagePath) {
            string tempFolder = packagePath + ".extracted/";
            if (Directory.Exists(tempFolder)) {
                Directory.Delete(tempFolder, true);
            }
            using (ZipFile zip = new ZipFile(packagePath)) {
                foreach (ZipEntry entry in zip) {
                    if (!entry.FileName.Contains("/")) {
                        entry.Extract(tempFolder, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            string uninstallPackagePath = this.packageListDirectory + "UpdaterCache/" + result.PackagesToUpdate[currentPackage].FullID + ".udruninsta_";
            if (System.IO.File.Exists(uninstallPackagePath)) {
                // An uninstall package exists!
                // Try to uninstall it
                UninstallPackage(result.PackagesToUpdate[currentPackage], uninstallPackagePath);
            }
            PackageLoader loader = new PackageLoader();
            loader.LoadPackage(tempFolder + result.PackagesToUpdate[currentPackage].FullID + ".dll");
            loader.Package.Initialize(this);
            PackageInstaller installer = new PackageInstaller(packagePath, tempFolder, this.packageListDirectory, result.PackagesToUpdate[currentPackage], loader);
            // Start installing the package
            loader.Package.Install(installer);
            // Installation complete! Add this package to the installed packages list
            MarkPackageInstalled(installer.Package.FullID, installer.Package.Version);
            SaveInstalledPackageList();
            // Close all resources used by the package installer
            installer.Close();
            loader.Unload();

            if (Directory.Exists(System.IO.Path.GetDirectoryName(uninstallPackagePath)) == false) {
                Directory.CreateDirectory(Path.GetDirectoryName(uninstallPackagePath));
            }
            using (ZipFile zip = new ZipFile()) {
                zip.AddFile(tempFolder + "FileList.xml", "\\");
                zip.AddFile(tempFolder + result.PackagesToUpdate[currentPackage].FullID + ".dll", "\\");
                zip.AddFile(tempFolder + "Updater.Linker.dll", "\\");

                zip.Save(uninstallPackagePath);
            }

            // Delete the temporary files
            if (Directory.Exists(tempFolder)) {
                Directory.Delete(tempFolder, true);
            }
            if (File.Exists(packagePath)) {
                File.Delete(packagePath);
            }
            OnPackageInstallationComplete(result, currentPackage);
        }

        private void OnPackageInstallationComplete(IUpdateCheckResult result, int currentPackage) {
            if (PackageInstallationComplete != null) {
                PackageInstallationComplete(this, new PackageInstallationCompleteEventArgs(result.PackagesToUpdate[currentPackage], currentPackage));
            }
            if (currentPackage + 1 < result.PackagesToUpdate.Count) {
                DownloadPackage(result, currentPackage + 1);
            } else {
                // Done the update!
                if (InstallationComplete != null) {
                    InstallationComplete(this, EventArgs.Empty);
                }
            }
        }

        private void UninstallPackage(IPackageInfo packageInfo, string uninstallPackagePath) {
            PackageFileList fileList = null;

            string tempFolder = uninstallPackagePath + ".extracted/";
            if (Directory.Exists(tempFolder)) {
                Directory.Delete(tempFolder, true);
            }
            using (ZipFile zip = new ZipFile(uninstallPackagePath)) {
                foreach (ZipEntry entry in zip) {
                    entry.Extract(tempFolder, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            using (FileStream stream = new FileStream(tempFolder + "FileList.xml", FileMode.Open, FileAccess.Read, FileShare.None)) {
                fileList = LoadPackageFileList(stream);
            }

            ExecuteUninstallation(tempFolder + packageInfo.FullID + ".dll", packageInfo, fileList);

            if (Directory.Exists(tempFolder)) {
                Directory.Delete(tempFolder, true);
            }
        }

        private PackageFileList LoadPackageFileList(Stream fileListStream) {
            PackageFileList fileList = new PackageFileList();
            using (XmlReader reader = XmlReader.Create(fileListStream)) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        switch (reader.Name) {
                            case "File": {
                                    PackageFileListItem item = new PackageFileListItem(this.packageListDirectory, reader.ReadString().TrimStart('\\'));
                                    fileList.Add(item);
                                }
                                break;
                        }
                    }
                }
            }
            return fileList;
        }

        private void ExecuteUninstallation(string packagePath, IPackageInfo packageInfo, PackageFileList fileList) {
            PackageLoader loader = new PackageLoader();
            loader.LoadPackage(packagePath);
            loader.Package.Initialize(this);
            PackageUninstaller uninstaller = new PackageUninstaller(packageInfo, fileList);
            // Start installing the package
            loader.Package.Uninstall(uninstaller);
            // Uninstallation complete! Add this package to the installed packages list
            int removalCount = MarkPackageUninstalled(uninstaller.Package.FullID);
            if (removalCount > 0) {
                SaveInstalledPackageList();
            }
            loader.Unload();
        }

        public bool IsPackageInstalled(string packageID, int version) {
            for (int i = 0; i < installedPackages.Count; i++) {
                if (installedPackages[i].MatchIfNewer(packageID, version)) {
                    return true;
                }
            }
            return false;
        }

        private void MarkPackageInstalled(string packageID, int version) {
            bool updated = false;
            for (int i = 0; i < installedPackages.Count; i++) {
                if (installedPackages[i].FullID == packageID) {
                    installedPackages[i].Version = version;
                    updated = true;
                }
            }
            if (updated == false) {
                installedPackages.Add(new InstalledPackageInfo(packageID, version));
            }
        }

        private int MarkPackageUninstalled(string packageID) {
            int removalCount = 0;
            for (int i = installedPackages.Count - 1; i >= 0; i--) {
                if (installedPackages[i].FullID == packageID) {
                    installedPackages.RemoveAt(i);
                    removalCount++;
                }
            }
            return removalCount;
        }

        public void UpdateStatus(string status) {
            this.status = status;
            if (StatusUpdated != null) {
                StatusUpdated(this, EventArgs.Empty);
            }
        }

        public void LoadInstalledPackageList() {
            // Check if the configuration file exists, if not, create it
            if (System.IO.File.Exists(packageListDirectory + "packages.dat") == false) {
                // Package list doesn't exist! PANIC!
                SaveInstalledPackageList();
            }
            // Clear the in-memory installed package list
            installedPackages.Clear();
            // Decrypt the configuration file into a string
            string xmlData = System.Text.Encoding.Unicode.GetString(packageListEncryption.DecryptBytes(System.IO.File.ReadAllBytes(packageListDirectory + "packages.dat")));
            using (XmlTextReader reader = new XmlTextReader(new System.IO.StringReader(xmlData))) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        switch (reader.Name) {
                            case "Package": {
                                    installedPackages.Add(new InstalledPackageInfo("", 1));
                                }
                                break;
                            case "ID": {
                                    installedPackages[installedPackages.Count - 1].FullID = reader.ReadString();
                                }
                                break;
                            case "Version": {
                                    installedPackages[installedPackages.Count - 1].Version = reader.ReadString().ToInt();
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void SaveInstalledPackageList() {
            StringBuilder output = new StringBuilder();
            // Write a new xml document to the 'output' StringBuilder
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "   ";
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(output, settings)) {
                writer.WriteStartDocument();
                // Write the root node
                writer.WriteStartElement("PackageInfo");
                // Write the 'InstalledPackages' node
                writer.WriteStartElement("InstalledPackages");
                for (int i = 0; i < installedPackages.Count; i++) {
                    writer.WriteStartElement("Package");

                    writer.WriteElementString("ID", installedPackages[i].FullID);
                    writer.WriteElementString("Version", installedPackages[i].Version.ToString());

                    writer.WriteEndElement();
                }
                // Close the 'InstalledPackages' node
                writer.WriteEndElement();

                // Close the root node
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            // Encrypt the xml string and write the encrypted value to the file
            System.IO.File.WriteAllBytes(packageListDirectory + "packages.dat", packageListEncryption.EncryptBytes(System.Text.Encoding.Unicode.GetBytes(output.ToString())));
        }

        private bool CanInstallPackage(IPackageInfo package) {
            if (!IsPackageInstalled(package.FullID, package.Version)) {
                // This package is not already installed
                for (int i = 0; i < package.Prerequisites.Count; i++) {
                    // Check if all of the prerequisites as installed
                    if (!IsPackageInstalled(package.Prerequisites[i].FullID, package.Prerequisites[i].Version)) {
                        // A prerequisite is not installed! That means we cannot install this package
                        return false;
                    }
                }
                // All of the prerequisites are installed! That means we can install this package
                return true;
            } else {
                // The package is installed. We don't need to install it again
                return false;
            }
        }

        private List<IPackageInfo> ReadUpdateFile(UpdateCheckResult updateResult) {
            List<IPackageInfo> packageInfoList = new List<IPackageInfo>();

            using (XmlReader reader = XmlReader.Create(updateResult.UpdateURI)) {
                while (reader.Read()) {
                    if (reader.IsStartElement()) {
                        switch (reader.Name) {
                            case "Package": {
                                    PackageInfo package = new PackageInfo();
                                    package.ReadBasicFromXml(reader);
                                    packageInfoList.Add(package);

                                }
                                break;
                        }
                    }
                }
            }

            return packageInfoList;
        }

        private string Substring(string String, int Start, int End) {
            //If start is after the end then just flip the values
            if (Start > End) {
                Start = Start ^ End;
                End = Start ^ End;
                Start = Start ^ End;
            }
            if (End > String.Length) End = String.Length;
            //if the end is outside of the string, just make it the end of the string
            return String.Substring(Start, End - Start);
        }

        #endregion Methods


        public void ExtractPackageFile(string packagePath, string fileToExtract, string destinationPath) {

        }
    }
}