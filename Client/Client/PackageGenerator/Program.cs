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
using Ionic.Zip;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;

namespace PMDCP.Updater.PackageGenerator
{
    class Program
    {
        static Command commandLine;

        static void Main(string[] args) {
            try {
                commandLine = CommandProcessor.ParseCommand(Environment.CommandLine);
                string packageDll = null;
                string outputFile = null;
                string dataDirectory = null;
                int index = commandLine.FindCommandArg("/dll");
                if (index > -1) {
                    packageDll = commandLine[index + 1];
                }
                index = commandLine.FindCommandArg("/out");
                if (index > -1) {
                    outputFile = commandLine[index + 1];
                }
                index = commandLine.FindCommandArg("/data");
                if (index > -1) {
                    dataDirectory = commandLine[index + 1];
                }

                if (!string.IsNullOrEmpty(packageDll)) {
                    packageDll = Path.GetFullPath(packageDll);
                }
                if (!string.IsNullOrEmpty(outputFile)) {
                    outputFile = Path.GetFullPath(outputFile);
                }
                if (!string.IsNullOrEmpty(dataDirectory)) {
                    dataDirectory = Path.GetFullPath(dataDirectory);
                }

                CreatePackage(packageDll, outputFile, dataDirectory);
                string xmlOutputFile = null;
                index = commandLine.FindCommandArg("/xmlout");
                if (index > -1) {
                    xmlOutputFile = commandLine[index + 1];
                }
                if (!string.IsNullOrEmpty(xmlOutputFile)) {
                    xmlOutputFile = Path.GetFullPath(xmlOutputFile);
                    CreatePackageXml(xmlOutputFile, outputFile);
                }
                GeneratePackageGenLaunchScript(Path.GetDirectoryName(outputFile) + "\\PackageGen.bat", args);
            } catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        static void GeneratePackageGenLaunchScript(string destinationPath, string[] args) {
            string comLine = null;
            for (int i = 0; i < args.Length; i++) {
                if (args[i].StartsWith("/")) {
                    comLine += " " + args[i];
                } else {
                    if (System.IO.File.Exists(args[i]) || System.IO.Directory.Exists(args[i])) {
                        args[i] = Path.GetFullPath(args[i]);
                        string relativePath = GetRelativePath(args[i], Path.GetDirectoryName(destinationPath)); 
                        comLine += " \"" + relativePath + "\"";
                    } else {
                        comLine += " \"" + args[i] + "\"";
                    }
                }
            }
            File.WriteAllText(destinationPath, "Start " + GetRelativePath(Application.ExecutablePath, Path.GetDirectoryName(destinationPath)) + "" + comLine);
        }

        static string GetRelativePath(string absolutePath, string relativeTo) {
            string[] absoluteDirectories = relativeTo.Split('\\');
            string[] relativeDirectories = absolutePath.Split('\\');

            //Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
                throw new ArgumentException("Paths do not have a common base");

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                if (absoluteDirectories[index].Length > 0)
                    relativePath.Append("..\\");

            //Add on the folders
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                relativePath.Append(relativeDirectories[index] + "\\");
            relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }

        static void CreatePackage(string packageDll, string outputFile, string dataDirectory) {
            // Check if the dll was specified
            if (string.IsNullOrEmpty(packageDll)) {
                Console.WriteLine("No executable dll specified. Unable to create package.");
            }
            // Check if the output file was specified
            if (string.IsNullOrEmpty(outputFile)) {
                Console.WriteLine("No output file specified. Unable to create package.");
            }
            // Check if the dll exists
            if (!File.Exists(packageDll)) {
                Console.WriteLine("Specified dll not found. Unable to create package.");
            }
            // Check if the output file directory exists, if not, create it
            if (!Directory.Exists(Path.GetDirectoryName(outputFile))) {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
            }
            // All data is ready! Now let's create the package.
            Console.WriteLine("Creating package...");
            CreatePackageStructure(packageDll, outputFile, dataDirectory);
        }

        static void CreatePackageStructure(string packageDll, string outputFile, string dataDirectory) {
            if (File.Exists(outputFile)) {
                File.Delete(outputFile);
            }
            if (!string.IsNullOrEmpty(dataDirectory) && Directory.Exists(dataDirectory) == false) {
                Directory.CreateDirectory(dataDirectory);
            }
            using (ZipFile zip = new ZipFile(outputFile)) {
                zip.AddFile(packageDll, "\\");
                zip.AddEntry("Config.xml", CreatePackageConfigXml(packageDll).ToString());
                zip.AddEntry("FileList.xml", CreatePackageFileList(dataDirectory));
                if (!string.IsNullOrEmpty(dataDirectory) && Directory.GetFiles(dataDirectory, "*", SearchOption.AllDirectories).Length > 0) {
                    Console.WriteLine("Adding data files to package...");
                    string[] files = Directory.GetFiles(dataDirectory, "*", SearchOption.AllDirectories);
                    for (int i = 0; i < files.Length; i++) {
                        if (!files[i].Contains(".svn")) {
                            zip.AddFile(files[i], "\\Files" + Path.GetDirectoryName(files[i].Replace(dataDirectory, "")));
                        }
                    }
                } else {
                    zip.AddDirectoryByName("Files");
                }
                Assembly assembly = Assembly.LoadFile(packageDll);
                Console.WriteLine("Adding references to package...");
                foreach (AssemblyName name in assembly.GetReferencedAssemblies()) {
                    if (name.Name != "mscorlib" && name.Name != "System") {
                        zip.AddFile(Path.GetDirectoryName(packageDll) + "\\" + name.Name + ".dll", "\\");
                    }
                }
                Console.WriteLine("Saving package...");
                zip.Save();
            }
        }

        static StringBuilder CreatePackageConfigXml(string packageDll) {
            Console.WriteLine("Generating configuration xml...");
            StringBuilder output = new StringBuilder();
            // Write a new xml document to the 'output' StringBuilder
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "   ";
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(output, settings)) {
                writer.WriteStartDocument();
                // Write the root node
                writer.WriteStartElement("Data");

                writer.WriteStartElement("PackageInfo");

                //writer.WriteElementString("Test", "Test Value!");

                writer.WriteEndElement();

                // Close the root node
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return output;
        }

        static void CreatePackageXml(string path, string packagePath) {
            Console.WriteLine("Generating update xml...");
            string configFile = commandLine["/config"];
            string name = null;
            string description = null;
            if (!string.IsNullOrEmpty(configFile) && System.IO.File.Exists(configFile)) {
                // The config file was found, load config from it
                using (XmlReader reader = XmlReader.Create(configFile)) {
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
                            }
                        }
                    }
                }
            } else {
                // The config file does not exist, try to load config from command line
                name = commandLine["/name"];
                description = commandLine["/desc"];
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "   ";
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(path, settings)) {
                writer.WriteStartDocument();
                // Write the root node
                writer.WriteStartElement("Data");

                writer.WriteStartElement("Package");

                writer.WriteElementString("ID", Path.GetFileNameWithoutExtension(packagePath));
                if (!string.IsNullOrEmpty(name)) {
                    writer.WriteElementString("Name", name);
                }
                if (!string.IsNullOrEmpty(description)) {
                    writer.WriteElementString("Description", description);
                }
                writer.WriteElementString("Hash", Security.Hash.GetFileHash(packagePath, Security.HashType.SHA1));
                writer.WriteElementString("Size", (new FileInfo(packagePath)).Length.ToString());
                writer.WriteElementString("PublishDate", DateTime.UtcNow.ToLongDateString());

                writer.WriteEndElement();

                // Close the root node
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        static byte[] CreatePackageFileList(string dataDirectory) {
            PackageFileList fileList = new PackageFileList();
            string[] files = Directory.GetFiles(dataDirectory, "*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++) {
                if (!files[i].Contains(".svn")) {
                    fileList.Add(new PackageFileListItem("", Path.GetDirectoryName(files[i].Replace(dataDirectory, "")) + "/" + System.IO.Path.GetFileName(files[i])));
                }
            }
            using (MemoryStream memoryStream = new MemoryStream()) {
                fileList.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
