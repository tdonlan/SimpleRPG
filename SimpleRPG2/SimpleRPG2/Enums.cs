﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
   public enum CharacterType
   {
       Player,
       Enemy,
       NPC
   }

    public enum ItemType
    {
        Weapon,
        Potion,
        Armor,
    }

    public enum StatType
    {
        Armor,
        HitPoints,
        Attack
    }

    public enum BattleStatusType
    {
        PlayersDead,
        EnemiesDead,
        Running,
       
    }
}
