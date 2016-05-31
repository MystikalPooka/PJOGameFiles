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

namespace PMDCP.Updater.Linker
{
    public static class Extensions
    {
        public static int ToInt(this string str) {
            int result = 0;
            if (str != null && int.TryParse(str, out result) == true) {
                return result;
            } else
                return 0;
        }

        public static int ToInt(this string str, int defaultVal) {
            int result = 0;
            if (str != null && int.TryParse(str, out result) == true) {
                return result;
            } else
                return defaultVal;
        }

        public static long ToLong(this string str) {
            long result = 0;
            if (str != null && long.TryParse(str, out result) == true) {
                return result;
            } else
                return 0;
        }

        public static long ToLong(this string str, long defaultVal) {
            long result = 0;
            if (str != null && long.TryParse(str, out result) == true) {
                return result;
            } else
                return defaultVal;
        }

        public static double ToDbl(this string str) {
            double result = 0;
            if (str != null && double.TryParse(str, out result) == true) {
                return result;
            } else
                return 0;
        }

        public static double ToDbl(this string str, double defaultVal) {
            double result = 0;
            if (str != null && double.TryParse(str, out result) == true) {
                return result;
            } else
                return defaultVal;
        }

        public static string ToIntString(this bool boolval) {
            if (boolval == true)
                return "1";
            else
                return "0";
        }

        public static bool IsNumeric(this string str) {
            int result;
            return int.TryParse(str, out result);
        }

        public static ulong ToUlng(this string str) {
            ulong result = 0;
            if (ulong.TryParse(str, out result) == true) {
                return result;
            } else
                return 0;
        }

        public static bool ToBool(this string str) {
            switch (str.ToLower()) {
                case "true":
                    return true;
                case "false":
                    return false;
                case "1":
                    return true;
                case "0":
                    return false;
                default:
                    return false;
            }
        }

        public static DateTime? ToDate(this string date) {
            DateTime tmpDate;
            if (DateTime.TryParse(date, out tmpDate)) {
                return tmpDate;
            } else {
                return null;
            }
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ExtensionAttribute : Attribute
    {
    }
}

