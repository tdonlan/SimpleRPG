using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class CharacterFactory
    {
        public static GameCharacter getEnemy(Random r)
        {
            GameCharacter retval = new GameCharacter() {name="Goblin",displayChar='G',type=CharacterType.Enemy, ac = 10, attack = 5, totalHP = 10, hp = 10, ap=10, totalAP=10 };
            retval.weapon = ItemFactory.getWeapon(r);
            return retval;
        }

        public static GameCharacter getPlayerCharacter(Random r)
        {
            GameCharacter retval =  new GameCharacter() {name="Warrior",displayChar='@',type=CharacterType.Player, ac = 10, attack = 50, totalHP = 50, hp = 50,ap=10,totalAP=10 };
        
            retval.inventory.Add(ItemFactory.getHealingPotion(r));

            Armor a = ItemFactory.getArmor(r);
            retval.inventory.Add(a);
            retval.EquipArmor(a);

            retval.weapon = ItemFactory.getWeapon(r);

            /*
            retval.abilityList.Add(AbilityFactory.getFireball());
            retval.abilityList.Add(AbilityFactory.getTeleport());
            retval.abilityList.Add(AbilityFactory.getKnockback());
            retval.abilityList.Add(AbilityFactory.getCharge());
            retval.abilityList.Add(AbilityFactory.getGrenade());
            retval.abilityList.Add(AbilityFactory.getShield());
            retval.abilityList.Add(AbilityFactory.getRage());
            */
            retval.abilityList.Add(AbilityFactory.getWeb());
            retval.abilityList.Add(AbilityFactory.getDispellMagic());
           

            return retval;
        }

    }
}
