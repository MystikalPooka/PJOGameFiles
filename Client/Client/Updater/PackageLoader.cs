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
    using System.IO;
    using System.Reflection;
    using System.Text;

    using PMDCP.Updater.Linker;

    public class PackageLoader : MarshalByRefObject, IPackageLoader
    {
        #region Fields

        string assemblyPath;
        IUpdatePackage package;
        AppDomain packageAppDomain;

        #endregion Fields

        #region Properties

        public string AssemblyPath {
            get { return assemblyPath; }
        }

        public IUpdatePackage Package {
            get { return package; }
        }

        public AppDomain PackageAppDomain {
            get { return packageAppDomain; }
        }

        #endregion Properties

        #region Methods

        public void LoadPackage(string assemblyPath) {
            if (packageAppDomain != null) {
                Unload();
            }
            this.assemblyPath = assemblyPath;
            string fileNamespace = Path.GetFileNameWithoutExtension(assemblyPath);
            string dir = Path.GetDirectoryName(assemblyPath);

            AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            if (false) {
                setup.ApplicationBase = Environment.CurrentDirectory;
            } else {
                setup.ApplicationBase = dir;
            }
            packageAppDomain = AppDomain.CreateDomain(fileNamespace, null, setup);
            //this.packageAppDomain.Load()
            this.package = this.packageAppDomain.CreateInstanceFrom(assemblyPath, "UpdatePackage.UpdatePackage").Unwrap() as IUpdatePackage;
        }

        public void LoadPackage(byte[] assembly, IPackageInfo packageInfo) {
            if (packageAppDomain != null) {
                Unload();
            }
            this.assemblyPath = assemblyPath;
            string fileNamespace = packageInfo.FullID;
            string dir = Path.GetDirectoryName(assemblyPath);

            //AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;
            //setup.ApplicationBase = Environment.CurrentDirectory;
            packageAppDomain = AppDomain.CreateDomain(fileNamespace, null);
            // TODO: Load all the of the dependancies into memory too!
            //this.packageAppDomain.Load()
            Assembly loadedAssembly = this.packageAppDomain.Load(assembly);
            this.package = this.packageAppDomain.CreateInstance(loadedAssembly.FullName, "UpdatePackage.UpdatePackage").Unwrap() as IUpdatePackage;
        }

        public void Unload() {
            if (packageAppDomain != null) {
                try {
                    AppDomain.Unload(packageAppDomain);
                } catch { }
                //packageAppDomain = null;
                //package = null;
            }
        }

        #endregion Methods
    }
}