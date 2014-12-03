using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class AI
    {

        public static void attackNearestPlayer(GameCharacter enemy, BattleGame game)
        {
            
            var attackTarget= getAttackablePlayer(enemy,game);
            if (attackTarget != null)
            {
                attackPlayer(enemy, attackTarget, game.battleLog, game.r);
            }
            else
            {
                var moveTarget = findNearestPlayer(enemy, game.board, game.characterList);
                moveToPlayer(enemy, moveTarget, game.board);
            }
            
        }

        private static void attackPlayer(GameCharacter enemy, GameCharacter player, BattleLog log, Random r)
        {
            if(enemy.SpendAP(enemy.weapon.actionPoints))
            {
                CombatHelper.Attack(enemy, player, log, r);
            }
        }

        public static GameCharacter getAttackablePlayer(GameCharacter enemy, BattleGame game)
        {
            Tile curTile = game.board.getTileFromLocation(enemy.x,enemy.y);
            var charList = game.getCharactersFromTileList(game.board.getTileListFromPattern(curTile, TilePatternType.FourAdj));

            //for now, just return a random enemy close 
            if (charList.Count > 0)
            {
                return charList[game.r.Next(charList.Count - 1)];
            }
            else
            {
                return null;
            }

        }


        public static GameCharacter findNearestPlayer( GameCharacter enemy, Board board,List<GameCharacter> charList)
        {
            GameCharacter retval = null;
            int dist = 999;
            
            foreach(GameCharacter c in charList)
            {
                if (c.type == CharacterType.Player)
                {
                    var pointList = PathFind.Pathfind(board, enemy.x, enemy.y, c.x, c.y);
                    if (pointList.Count < dist)
                    {
                        dist = pointList.Count;
                        retval = c;
                    }
                }
               
            }
            return retval;
        }

        //iterates over the path find and moves single spaces
        public static void moveToPlayer(GameCharacter enemy, GameCharacter target, Board board)
        {
            var pointList = PathFind.Pathfind(board, enemy.x, enemy.y, target.x, target.y);
            foreach(var p in pointList)
            {
                if(enemy.SpendAP(1))
                {
                    board.MoveCharacter(enemy, board.getTileFromLocation(p.x, p.y));
                }
                else
                {
                    return;
                }
            }
        }

    }
}
