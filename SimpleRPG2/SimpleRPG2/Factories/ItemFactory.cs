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

        public static Weapon getWeapon(Random r)
        {
            Weapon w = new Weapon() {name="Long Sword",minDamage=5,maxDamage=10, type=ItemType.Weapon,actionPoints=2,activeEffects=null,passiveEffects=null };
            return w;

        }

        public static Armor getArmor(Random r)
        {
            Armor a = new Armor() {name="Chain Mail",activeEffects=null,armor=10,passiveEffects=null,type=ItemType.Armor, armorType=ArmorType.Chest };
            return a;
        }
    }
}
