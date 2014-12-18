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

    public enum ArmorType
    {
        Head,
        Chest,
        Gloves,
        Legs,
        Boots,
        Ring,
        Trinket,
    }

    public enum StatType
    {
        ActionPoints,
        Armor,
        Damage, 
        Heal,
        HitPoints, //temp buff to hitpoints
        Attack,
        Teleport, //move character to target
        Knockback, // move targets away from character
        Explode, //move characters away from target
        Stuck, //character cannot move
        Dispell, //remove active effects
       
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
        LOSEmpty,
        LOSTarget,
    }

}

