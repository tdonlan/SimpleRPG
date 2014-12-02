using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class ItemFactory
    {
        public Item getItem(Random r)
        {
            Item i = new Item() {name="Healing Potion",activeEffects=new List<ActiveEffect>(){getActiveEffect(r)},passiveEffects=null,type=ItemType.Potion };

            return i;
        }

        public ActiveEffect getActiveEffect(Random r)
        {
            return new ActiveEffect() {name="Heal",amount=5,duration=1,statType=StatType.HitPoints };
        }
        
        public PassiveEffect getPassiveEffect(Random r)
        {
            return new PassiveEffect() { name = "Regen", amount = 1, statType = StatType.HitPoints };
        }
        
        public Weapon getWeapon(Random r)
        {
            Weapon w = new Weapon() {name="Long Sword",damage=6,type=ItemType.Weapon,actionPoints=5,activeEffects=null,passiveEffects=null };
            return w;

        }

        public Armor getArmor (Random r)
        {
            Armor a = new Armor() {name="Chain Mail",activeEffects=null,armor=10,passiveEffects=null,type=ItemType.Armor };
            return a;
        }
    }
}
