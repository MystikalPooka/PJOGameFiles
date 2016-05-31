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

namespace PMDCP.Updater.PackageGenerator
{
    public class CommandProcessor
    {
        public static string[] SplitCommand(string fullText) {
            fullText = fullText.Trim(' ');
            if (fullText.Contains(" ")) {
                List<string> parsed = new List<string>();
                bool startNewLine = true;
                int currentLine = -1;
                bool isInQuotes = false;
                for (int i = 0; i < fullText.Length; i++) {
                    if (startNewLine) {
                        parsed.Add("");
                        currentLine++;
                        startNewLine = false;
                    }
                    char curChar = fullText[i];
                    if (curChar == ' ' && isInQuotes == false) {
                        startNewLine = true;
                    } else if (curChar == '"') {
                        isInQuotes = !isInQuotes;
                    } else {
                        parsed[currentLine] += curChar;
                    }
                }
                return parsed.ToArray();
            } else {
                return new string[] { fullText };
            }
        }

        public static string JoinArgs(string[] args) {
            string joinedArgs = "";
            for (int i = 1; i < args.Length; i++) {
                joinedArgs += args[i] + " ";
            }
            return joinedArgs;
        }

        /// <summary>
        /// Parses the command
        /// </summary>
        /// <returns>The parsed command arguments</returns>
        public static Command ParseCommand(string command) {
            string fullCommandLine = command.Trim();
            List<string> parsed = new List<string>();
            bool startNewLine = true;
            int currentLine = -1;
            bool isInQuotes = false;
            for (int i = 0; i < fullCommandLine.Length; i++) {
                if (startNewLine) {
                    parsed.Add("");
                    currentLine++;
                    startNewLine = false;
                }
                char curChar = fullCommandLine[i];
                if (curChar == ' ' && isInQuotes == false) {
                    startNewLine = true;
                } else if (curChar == '"') {
                    isInQuotes = !isInQuotes;
                } else {
                    parsed[currentLine] += curChar;
                }
            }
            return new Command(fullCommandLine, parsed);
        }
    }
}
