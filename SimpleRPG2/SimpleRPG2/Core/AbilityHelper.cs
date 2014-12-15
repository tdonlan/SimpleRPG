using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class AbilityHelper
    {
        private static bool UseAbilityLOS(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            Tile ActiveTile = game.board.getTileFromLocation(character.x, character.y);
            var tileLOSList = game.board.getBoardLOS(ActiveTile, target);

            if (tileLOSList[tileLOSList.Count - 1] == target)
            {
                if (character.SpendAP(ability.ap))
                {
                    //dont spend uses for now.
                    return UseAbilityAOEHelper(character, ability, target, game);
                }
                return false;
            }
            else
            {
                return false;
            }

        }

        private static bool UseAbilityAOEHelper(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {

            TilePatternType patternType = TilePatternType.Single;

            switch (ability.targetType)
            {
                case AbilityTargetType.LOSAOE:
                    patternType = ability.tilePatternType;
                    break;
                case AbilityTargetType.PointAOE:
                    patternType = ability.tilePatternType;
                    break;

                default:
                    return false;

            }

            //get tile pattern from AOE
            var tileAOEList = game.board.getTileListFromPattern(target, ability.tilePatternType);
            var charAOEList = game.getCharactersFromTileList(tileAOEList);

            return UseAbilityOnCharList(ability, charAOEList, game);

        }

        //Special case for movement / teleport abilities
        private static bool UseAbilityPointHelper(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            foreach(ActiveEffect ae in ability.activeEffects)
            {
                if(ae.statType == StatType.MoveSelf)
                {
                    game.board.MoveCharacterFree(character, target);
                }
                else if(ae.statType == StatType.MoveTarget)
                {
                    //calculate the destination based on character / target and range.
                    Tile ActiveTile = game.board.getTileFromLocation(character.x,character.y);
                    GameCharacter targetChar = game.getCharacterFromTile(target);
                    List<Tile> moveTargetList = game.board.getMoveTargetTileList(ActiveTile, target, ae.amount);
                    if(moveTargetList.Count >0)
                    {
                        Tile moveTile = moveTargetList[moveTargetList.Count-1];
                        game.board.MoveCharacterFree(targetChar,moveTile);
                    }
                }
                else
                {
                    GameCharacter targetChar = game.getCharacterFromTile(target);
                    if(targetChar != null)
                    {
                        targetChar.AddActiveEffect(ae,game);
                    }
                }
            }

            return true;
        }

        private static bool UseAbilityOnCharList(Ability ability, List<GameCharacter> characterList, BattleGame game)
        {
            foreach (var character in characterList)
            {
                foreach (var a in ability.activeEffects)
                {
                    character.AddActiveEffect(a, game);
                }
            }

            return true;
        }

        private static bool UseAbilityPoint(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            if (character.SpendAP(ability.ap))
            {
                return UseAbilityAOEHelper(character, ability, target, game);
            }
            return false;
        }

        private static bool UseAbilityPointEmpty(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            if(target.empty)
            {
                if (character.SpendAP(ability.ap))
                {
                    UseAbilityPointHelper(character, ability, target, game);
                }
            }
          
            return false;
        }

        //Includes Self
        private static bool UseAbilityAllFriends(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            if (character.SpendAP(ability.ap))
            {
                var friendList = from data in game.characterList
                                 where data.type == character.type
                                 select data;

                return UseAbilityOnCharList(ability, friendList.ToList(), game);
            }
            else
            {
                return false;
            }
        }

        private static bool UseAbilityAllFoes(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            if (character.SpendAP(ability.ap))
            {
                var foeList = from data in game.characterList
                              where data.type != character.type
                              select data;

                return UseAbilityOnCharList(ability, foeList.ToList(), game);
            }
            else
            {
                return false;
            }
        }

        private static bool UseAbilitySingleFriend(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            if (character.SpendAP(ability.ap))
            {
                GameCharacter targetChar = game.getCharacterFromTile(target);
                if (targetChar != null & targetChar.type == character.type)
                {
                    return UseAbilityOnCharList(ability, new List<GameCharacter>() { targetChar }, game);
                }
            }
            return false;
        }

        private static bool UseAbilitySingleFoe(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            if (character.SpendAP(ability.ap))
            {
                GameCharacter targetChar = game.getCharacterFromTile(target);
                if (targetChar != null & targetChar.type != character.type)
                {
                    return UseAbilityOnCharList(ability, new List<GameCharacter>() { targetChar }, game);
                }
            }
            return false;
        }

        private static bool UseAbilitySelf(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            if (character.SpendAP(ability.ap))
            {
                return UseAbilityOnCharList(ability, new List<GameCharacter>() { character }, game);
            }
            else
            {
                return false;
            }
        }

        public static bool UseAbility(GameCharacter character, Ability ability, Tile target, BattleGame game)
        {
            switch (ability.targetType)
            {
                case AbilityTargetType.Self:
                    return UseAbilitySelf(character, ability, target, game);
                case AbilityTargetType.SingleFriend:
                    return UseAbilitySingleFriend(character, ability, target, game);
                case AbilityTargetType.SingleFoe:
                    return UseAbilitySingleFoe(character, ability, target, game);
                case AbilityTargetType.AllFriends:
                    return UseAbilityAllFriends(character, ability, target, game);
                case AbilityTargetType.AllFoes:
                    return UseAbilityAllFoes(character, ability, target, game);
                case AbilityTargetType.PointEmpty:
                    return UseAbilityPointEmpty(character, ability, target, game);
                case AbilityTargetType.PointTarget:
                    return UseAbilityPoint(character, ability, target, game);
                case AbilityTargetType.LOSTarget:
                    return UseAbilityLOS(character, ability, target, game);
                case AbilityTargetType.PointAOE:
                    return UseAbilityPoint(character, ability, target, game);
                case AbilityTargetType.LOSAOE:
                    return UseAbilityLOS(character, ability, target, game);
                default:
                    return false;
            }
        }

    }
}
