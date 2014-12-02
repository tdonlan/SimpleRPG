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
            //check if we are close

            //if we aren't, try to move into range

            if(r.Next(20) + attacker.attack > defender.ac)
            {
                int dmg = r.Next(attacker.weapon.damage);
                defender.hp -= dmg;
                log.AddEntry(string.Format("{0} hit {1} for {2} damage.", attacker.name, defender.name, dmg));
            }
        }

        public static bool Move(GameCharacter attacker, Tile destination)
        {
            //implement A* to find path
            //or just make players move single space at time?  (still need A* for AI)

            return false;


        }
    }
}
