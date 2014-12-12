using System;
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

    public enum CharacterClass
    {
        Warrior,
        Mage,
        Priest
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
        Attack,
        Movement
    }

    public enum BattleStatusType
    {
        PlayersDead,
        EnemiesDead,
        Running,
       
    }

    public enum DirectionType
    {
        North,
        South,
        West,
        East,
    }

    //patterns of tiles affected (by attacks, spells, etc)
    public enum TilePatternType
    {
        Single,
        FourAdj,
        EightAdj,
        NineSquare,
        ThreeLineVert,
        ThreeLineHor,
    }

    public enum AbilityTargetType
    {
        Self,
        SingleFriend,
        SingleFoe,
        AllFriends,
        AllFoes,
        PointEmpty,
        PointTarget,
        LOSTarget,
        PointAOE,
        LOSAOE,
    }

}

