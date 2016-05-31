﻿/*The MIT License (MIT)

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
using System.Collections.Generic;
using System.Text;

namespace Server.DataConverter.Npcs.V7 {
    public class Npc {
        public string Name { get; set; }
        public string AttackSay { get; set; }

        public int Form { get; set; }

        public Enums.NpcBehavior Behavior { get; set; }
        public int ShinyChance { get; set; }

        public int Species { get; set; }
        public bool SpawnsAtDay { get; set; }
        public bool SpawnsAtNight { get; set; }
        public bool SpawnsAtDawn { get; set; }
        public bool SpawnsAtDusk { get; set; }
        public NpcDrop[] Drops { get; set; }

        public int RecruitRate { get; set; }

        public int[] Moves { get; set; }

        public string AIScript { get; set; }

        public Npc() {
            Name = "";
            AttackSay = "";
            Drops = new NpcDrop[Constants.MAX_NPC_DROPS];
            for (int i = 0; i < Constants.MAX_NPC_DROPS; i++) {
                Drops[i] = new NpcDrop();
            }
            Moves = new int[4];
        }
    }
}
