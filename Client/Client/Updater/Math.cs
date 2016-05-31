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

namespace PMDCP.Updater
{
    public class Math
    {
        static readonly Random random = new Random(Environment.TickCount);

        public static int CalculatePercent(int currentValue, int maxValue) {
            return currentValue * 100 / maxValue;
        }

        public static ulong CalculatePercent(ulong currentValue, ulong maxValue) {
            return currentValue * 100 / maxValue;
        }

        public static int CalculatePercent(long currentValue, long maxValue) {
            return (int)(currentValue * 100 / maxValue);
        }

        public static int RoundToMultiple(int number, int multiple) {
            double d = number / multiple;
            d = System.Math.Round(d, 0);
            return Convert.ToInt32(d * multiple);
        }

        public static int Rand(int low, int high) {
            //lock (RandSyncObject) {
            return random.Next(low, high);
            //}
        }
    }
}
