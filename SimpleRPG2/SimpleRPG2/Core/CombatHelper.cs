using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class CombatHelper
    {
        public static void Attack(GameCharacter attacker, GameCharacter defender, BattleLog log, Random r)
        {

            if(r.Next(20) + attacker.attack > defender.ac)
            {
                int dmg = r.Next(attacker.weapon.damage)+1;
                defender.hp -= dmg;
                log.AddEntry(string.Format("{0} hit {1} for {2} damage.", attacker.name, defender.name, dmg));
            }
            else
            {
                log.AddEntry(string.Format("{0} missed {1}.", attacker.name, defender.name));
            }
        }
    }
}
