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
            GameCharacter retval =  new GameCharacter() {name="Warrior",displayChar='@',type=CharacterType.Player, ac = 10, attack = 5, totalHP = 10, hp = 5,ap=10,totalAP=10 };
        
            retval.inventory.Add(ItemFactory.getHealingPotion(r));
            retval.weapon = ItemFactory.getWeapon(r);

            retval.abilityList.Add(AbilityFactory.getFireball());
            retval.abilityList.Add(AbilityFactory.getTeleport());
                
            return retval;
        }

    }
}
