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

using PMU.Updater.Linker;
using System.IO;

namespace UpdatePackage
{
    public class UpdatePackage : MarshalByRefObject, IUpdatePackage
    {
        IUpdateClient updateClient;

        public void Initialize(IUpdateClient updateClient) {
            this.updateClient = updateClient;
        }

        public void Install(IPackageInstaller installer) {
            updateClient.UpdateStatus("Installing updater update...");
            string[] filesToInstall = new string[] { "Updater.dll", "Updater.Linker.dll" };
            for (int i = 0; i < filesToInstall.Length; i++) {
                string fullPath = installer.GetFullPath(filesToInstall[i]);
                if (installer.FileExists(fullPath, false)) {
                    System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcesses();
                    System.Diagnostics.Process myProc = System.Diagnostics.Process.GetCurrentProcess();
                    for (int n = 0; n < procs.Length; n++) {
                        if (procs[n].ProcessName == "PMU") {
                            if (procs[n].Id != myProc.Id) {
                                try {
                                    procs[n].Kill();
                                } catch { }
                            }
                        }
                    }
                    if (installer.FileExists(fullPath + ".ToDelete", false)) {
                        installer.DeleteFile(fullPath + ".ToDelete", false);
                    }
                    File.Move(fullPath, fullPath + ".ToDelete");
                }
            }
            installer.ExtractAll();
        }

        public void Uninstall(IPackageUninstaller uninstaller) {
            // Do nothing. This package cannot be uninstalled!
        }
    }
}
