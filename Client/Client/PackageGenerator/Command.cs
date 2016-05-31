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

namespace PMDCP.Updater.PackageGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Command
    {
        #region Fields

        List<string> commandArgs = new List<string>();
        string fullCommand;

        #endregion Fields

        #region Constructors

        internal Command(string fullCommand, List<string> command) {
            this.fullCommand = fullCommand;
            commandArgs = command;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the command line arguments for the program
        /// </summary>
        public List<string> CommandArgs {
            get { return commandArgs; }
        }

        /// <summary>
        /// Gets the full, unparsed command string
        /// </summary>
        public string FullCommand {
            get { return fullCommand; }
        }

        #endregion Properties

        #region Indexers

        public string this[int index] {
            get { return commandArgs[index]; }
        }

        public string this[string argument] {
            get {
                int index = FindCommandArg(argument);
                if (index > -1 && commandArgs.Count > index + 1) {
                    return commandArgs[index + 1];
                } else {
                    return null;
                }
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Checks if a certain argument is included in the command line
        /// </summary>
        /// <param name="argToFind">The argument to look for</param>
        /// <returns>True if the argument exists; False if it doesn't exist.</returns>
        public bool ContainsCommandArg(string argToFind) {
            return commandArgs.Contains(argToFind);
        }

        /// <summary>
        /// Retrives the index of a certain argument in the command line.
        /// </summary>
        /// <param name="argToFind"></param>
        /// <returns>The index of the argument if it was found; otherwise, returns -1</returns>
        public int FindCommandArg(string argToFind) {
            return commandArgs.IndexOf(argToFind);
        }

        #endregion Methods
    }
}