﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class BattleAction
    {
        public GameCharacter character { get; set; }
        public GameCharacter targetCharacter { get; set; }
        public Tile targetTile { get; set;}

        public BattleActionType actionType { get; set; }
        public Ability ability { get; set; }
        public UsableItem item {get;set;}



    }
}
