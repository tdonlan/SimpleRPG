using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class ItemFactory
    {
        public static Item getItem(Random r)
        {
            Item i = new Item() {name="Healing Potion",activeEffects=new List<ActiveEffect>(){getActiveEffect(r)},passiveEffects=null,type=ItemType.Potion };

            return i;
        }

        public static UsableItem getHealingPotion(Random r)
        {
            UsableItem i = new UsableItem() { name = "Healing Potion", activeEffects = new List<ActiveEffect>() { getActiveEffect(r) }, passiveEffects = null, type = ItemType.Potion, actionPoints=5,uses=1 };
            return i;
        }

        public static ActiveEffect getActiveEffect(Random r)
        {
            return new ActiveEffect() {name="Heal",minAmount=5,maxAmount=10,duration=1,statType=StatType.HitPoints };
        }

        public static PassiveEffect getPassiveEffect(Random r)
        {
            return new PassiveEffect() { name = "Regen", minAmount = 1, maxAmount=1, statType = StatType.HitPoints };
        }

        public static Weapon getLongsword(Random r)
        {
            Weapon w = new Weapon() {name="Long Sword",minDamage=5,maxDamage=10, type=ItemType.Weapon,actionPoints=2,activeEffects=null,passiveEffects=null };
            return w;
        }

        public static Weapon getDagger(Random r)
        {
            Weapon w = new Weapon() { name = "Dagger", minDamage = 1, maxDamage = 2, type = ItemType.Weapon, actionPoints = 1, activeEffects = null, passiveEffects = null };
            return w;
        }

        public static Weapon getBattleAxe(Random r)
        {
            Weapon w = new Weapon() { name = "Battle Axe", minDamage = 10, maxDamage = 20, type = ItemType.Weapon, actionPoints = 5, activeEffects = null, passiveEffects = null };
            return w;
        }

        #region Armor

        public static Armor getChainmail(Random r)
        {
            Armor a = new Armor() {name="Chain Mail",activeEffects=null,armor=10,passiveEffects=null,type=ItemType.Armor, armorType=ArmorType.Chest };
            return a;
        }

        public static Armor getLeatherChest(Random r)
        {
            Armor a = new Armor() { name = "Leather Chest", activeEffects = null, armor = 5, passiveEffects = null, type = ItemType.Armor, armorType = ArmorType.Chest };
            return a;
        }

        public static Armor getGreathelm(Random r)
        {
            Armor a = new Armor() { name = "Great Helm", activeEffects = null, armor = 5, passiveEffects = null, type = ItemType.Armor, armorType = ArmorType.Head };
            return a;
        }

        public static Armor getCap(Random r)
        {
            Armor a = new Armor() { name = "Leather Cap", activeEffects = null, armor = 1, passiveEffects = null, type = ItemType.Armor, armorType = ArmorType.Head };
            return a;
        }

        public static Armor getRegenRing(Random r)
        {
           
            ActiveEffect regenEffect = new ActiveEffect(){name="Regeneration",minAmount=1,maxAmount=1,statType=StatType.Heal};


            Armor a = new Armor() { name = "Regeneration Ring", activeEffects = new List<ActiveEffect>(){regenEffect}, armor = 0, 
                passiveEffects =null,
                type = ItemType.Armor, armorType = ArmorType.Ring };
            return a;
        }

        public static Armor getAttackRing(Random r)
        {
            PassiveEffect attackEffect = new PassiveEffect() { name = "Attack Ring", minAmount = 5, maxAmount = 5, statType = StatType.Attack };

            Armor a = new Armor() { name = "Attack Ring", activeEffects = null, armor = 0, passiveEffects = new List<PassiveEffect>() { attackEffect}, type = ItemType.Armor, armorType = ArmorType.Ring };
            return a;
        }


        #endregion
    }
}
