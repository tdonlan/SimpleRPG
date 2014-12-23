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

        public static bool RangedAttack(GameCharacter attacker, GameCharacter defender, Tile targetTile, BattleGame game)
        {
            bool retval = false;
            //check for ranged weapon
            if(attacker.weapon is RangedWeapon)
            {
                RangedWeapon w = (RangedWeapon)attacker.weapon;
                Ammo a = (Ammo)ItemHelper.getFirstItemWithID(attacker.inventory,attacker.Ammo.itemID);

                //check we have ammo 
                if(attacker.Ammo.count > 0 && a.ammoType == w.ammoType)
                {

                    List<Tile> tileLOSList = game.board.getBoardLOS(game.ActiveTile, targetTile);

                    //Draw Attack temp path
                    foreach (var t in tileLOSList)
                    {
                        game.board.AddTempChar(t, '*');
                    }

                    //check LOS
                    //check range
                    if (tileLOSList[tileLOSList.Count - 1] == targetTile )
                    {
                        if (tileLOSList.Count <= w.range)
                        {
                            if (attacker.SpendAP(attacker.weapon.actionPoints))
                            {
                                //check for hit
                                if (game.r.Next(20) + attacker.attack > defender.ac)
                                {
                                    int dmg = game.r.Next(attacker.weapon.minDamage, attacker.weapon.maxDamage) + a.bonusDamage;
                                    defender.Damage(dmg, game);

                                    game.battleLog.AddEntry(string.Format("{0} hit {1} for {2} damage.", attacker.name, defender.name, dmg));

                                    //remove ammo
                                    attacker.inventory.Remove(a);
                                    attacker.Ammo = ItemHelper.getItemSet(attacker.inventory, a);

                                    retval = true;
                                }
                                else
                                {
                                    game.battleLog.AddEntry(string.Format("{0} missed {1}.", attacker.name, defender.name));
                                }
                            }
                        }
                        else
                        {
                            game.battleLog.AddEntry(string.Format("{0} is out of range.", defender.name));
                        }
                    }
                    else
                    {
                        game.battleLog.AddEntry(string.Format("Unable to hit {0}", defender.name));
                    }
                }
                else
                {
                    game.battleLog.AddEntry(string.Format("{0} requires {1} ammo equipped", w.name,w.ammoType));
                }
            }
            else
            {
                game.battleLog.AddEntry(string.Format("Equip a ranged weapon for ranged attack"));
            }
           
            return retval;
        }
    }
}
