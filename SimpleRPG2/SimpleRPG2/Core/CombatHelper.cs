using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class CombatHelper
    {
        public static void Attack(GameCharacter attacker, GameCharacter defender, BattleGame game)
        {

            if(game.r.Next(20) + attacker.attack > defender.ac)
            {

                int dmg = game.r.Next(attacker.weapon.minDamage, attacker.weapon.maxDamage);
                defender.Damage(dmg,game);
               
                game.battleLog.AddEntry(string.Format("{0} hit {1} for {2} damage.", attacker.name, defender.name, dmg));
            }
            else
            {
                game.battleLog.AddEntry(string.Format("{0} missed {1}.", attacker.name, defender.name));
            }
        }
    }
}
