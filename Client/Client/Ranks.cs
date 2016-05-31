/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Drawing;
using Client.Logic.Players;

namespace Client.Logic
{
    /// <summary>
    /// Description of Ranks.
    /// </summary>
    internal class Ranks
    {
        /// <summary>
        /// Checks if the player has the specified rank permissions
        /// </summary>
        /// <param name="index">The index of the player to check</param>
        /// <param name="RankToCheck">The rank permissions to test</param>
        /// <returns>True if the player has the rank permissions; otherwise, false</returns>
        public static bool IsAllowed(IPlayer player, Enums.Rank RankToCheck)
        {
            Enums.Rank PlayerRank = player.Rank;
            switch (RankToCheck)
            {
                case Enums.Rank.Normal:
                    return true;
                case Enums.Rank.Moderator:
                    if (PlayerRank == Enums.Rank.Moderator || PlayerRank == Enums.Rank.Mapper || PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.Developer || PlayerRank == Enums.Rank.Owner || PlayerRank == Enums.Rank.Scripter)
                    {
                        return true;
                    }
                    break;
                case Enums.Rank.Mapper:
                    if (PlayerRank == Enums.Rank.Mapper || PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.Developer || PlayerRank == Enums.Rank.Owner || PlayerRank == Enums.Rank.Scripter)
                    {
                        return true;
                    }
                    break;
                case Enums.Rank.Developer:
                    if (PlayerRank == Enums.Rank.Developer || PlayerRank == Enums.Rank.Mapper || PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.Owner || PlayerRank == Enums.Rank.Scripter)
                    {
                        return true;
                    }
                    break;
                case Enums.Rank.Admin:
                    if (PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.Mapper || PlayerRank == Enums.Rank.Owner || PlayerRank == Enums.Rank.Scripter)
                    {
                        return true;
                    }
                    break;
                case Enums.Rank.Owner:
                    if (PlayerRank == Enums.Rank.Owner || PlayerRank == Enums.Rank.Scripter)
                    {
                        return true;
                    }
                    break;
                case Enums.Rank.Scripter:
                    if (PlayerRank == Enums.Rank.Scripter)
                    {
                        return true;
                    }
                    break;
                case Enums.Rank.Inspecter:
                    if (PlayerRank == Enums.Rank.Inspecter)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Checks if the player does not have the specified rank permissions
        /// </summary>
        /// <param name="index">The index of the player to check</param>
        /// <param name="RankToCheck">The rank permissions to test</param>
        /// <returns>True if the player does not have the rank permissions; otherwise, false</returns>
        public static bool IsDisallowed(IPlayer player, Enums.Rank RankToCheck)
        {
            return !IsAllowed(player, RankToCheck);
        }

        /// <summary>
        /// Gets the color associated with the specified rank
        /// </summary>
        /// <param name="rank">The rank used to determine the color returned</param>
        /// <returns>The color associated with the specified rank</returns>
        public static Color GetRankColor(Enums.Rank rank)
        {
            switch (rank)
            {
                case Enums.Rank.Normal:
                    return Color.Cyan;
                case Enums.Rank.Moderator:
                    return Color.Cyan;
                case Enums.Rank.Mapper:
                    return Color.Cyan;
                case Enums.Rank.Developer:
                    return Color.Cyan;
                case Enums.Rank.Admin:
                    return Color.Cyan;
                case Enums.Rank.Owner:
                    return Color.Cyan;
                case Enums.Rank.Scripter:
                    return Color.Cyan;
                case Enums.Rank.Inspecter:
                    return Color.Cyan;
                default:
                    return Color.DarkRed;
            }
        }
    }
}
