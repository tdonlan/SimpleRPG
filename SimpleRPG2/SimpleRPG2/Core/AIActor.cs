using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class AIActor
    {
        public GameCharacter character {get;set;}
        public EnemyType enemyType { get; set; }
        public Dictionary<AIActionType, int> actionWeight { get; set; }
        public List<AIAction> AIActionList { get; set; }

        public AIActor(GameCharacter character, EnemyType type)
        {
            this.character = character;
            this.enemyType = type;

            InitActionWeight();
            InitActionCost();
        }

        private void InitActionWeight()
        {
            actionWeight = AIFactory.getEnemyActionDictionary(enemyType);
        }

        private void InitActionCost()
        {
            AIActionList = new List<AIAction>();
            
        }


        private List<AIAction> getAIActions(BattleGame game, AIActionType type)
        {
            List<AIAction> AIActionList = new List<AIAction>();
            switch(type)
            {
                case AIActionType.Attack:
                    AIActionList.AddRange(getAIAttackActions(game));
                    break;
            }
            return AIActionList;
        }


        //if we have a melee weapon, calculate nearest enemy + weapon ap
        private List<AIAction> getAIAttackActions(BattleGame game)
        {
            List<AIAction> aiActionList = new List<AIAction>();
            if (character.weapon != null)
            {
                if (character.weapon.weaponType == WeaponType.OneHandMelee || character.weapon.weaponType == WeaponType.TwoHandMelee)
                {
                    GameCharacter targetCharacter = AI.findNearestPlayer(character, game.board, game.characterList);

                    List<Point> pointList = PathFind.Pathfind(game.board, character.x, character.y, targetCharacter.x, targetCharacter.y);
                    pointList.RemoveAt(0); //remove the character from pathfind.
                    pointList.RemoveAt(pointList.Count - 1); //remove the target from pathfind.

                    int dist = pointList.Count;

                    int cost = dist + character.weapon.actionPoints;

                    List<BattleAction> battleActionList = new List<BattleAction>();

                    foreach (var p in pointList)
                    {
                        battleActionList.Add(new BattleAction() { character = character, actionType = BattleActionType.Move, targetTile = game.board.getTileFromPoint(p) });
                    }

                    battleActionList.Add(new BattleAction() { character = character, targetCharacter = targetCharacter, targetTile = game.board.getTileFromLocation(targetCharacter.x, targetCharacter.y), actionType = BattleActionType.Attack });
                    
                    
                    aiActionList.Add(new AIAction() {actionType=AIActionType.Attack,cost=cost,battleActionList=battleActionList });
                }
            }

            return aiActionList;
        }

        //same as Melee attack, but add movement until we are LOS to nearest enemy
        private List<AIAction> getAIRangedAttackActions(BattleGame game)
        {
            List<AIAction> aiActionList = new List<AIAction>();
            if (character.weapon != null)
            {
                if (character.weapon.weaponType == WeaponType.OneHandRanged || character.weapon.weaponType == WeaponType.TwoHandRanged)
                {
                   
                    //should actually find who has easiest LOS
                    GameCharacter targetCharacter = AI.findNearestPlayer(character, game.board, game.characterList);

                    Tile characterTile = game.board.getTileFromLocation(character.x, character.y);
                    Tile targetTile = game.board.getTileFromLocation(targetCharacter.x, targetCharacter.y);
                    List<Point> pointList = game.board.getPathToLOS(characterTile, targetTile);

                    int dist = pointList.Count;

                    int cost = dist + character.weapon.actionPoints;

                    List<BattleAction> battleActionList = new List<BattleAction>();

                    foreach (var p in pointList)
                    {
                        battleActionList.Add(new BattleAction() { character = character, actionType = BattleActionType.Move, targetTile = game.board.getTileFromPoint(p) });
                    }

                    battleActionList.Add(new BattleAction() { character = character, targetCharacter = targetCharacter, targetTile = game.board.getTileFromLocation(targetCharacter.x, targetCharacter.y), actionType = BattleActionType.RangedAttack });


                    aiActionList.Add(new AIAction() { actionType = AIActionType.Attack, cost = cost, battleActionList = battleActionList });
                }
            }

            return aiActionList;
        }

        //get a list of all healing abilities / items.  
        //currently only abilities that are for self
        private List<AIAction> getAIHealActions(BattleGame game)
        {
            List<AIAction> aiActionList = new List<AIAction>();

            Tile targetTile = game.board.getTileFromLocation(character.x,character.y);

            foreach(var a in character.abilityList)
            {
                if(a.uses > 0 && a.canUseSelf())
                {
                    if(a.activeEffects.Select(x=>x.statType==StatType.Heal) != null)
                    {
                        List<BattleAction> battleActionList = new List<BattleAction>(){new BattleAction(){ability=a,character=character,targetCharacter=character,targetTile=targetTile,actionType=BattleActionType.UseAbility}};
                        aiActionList.Add(new AIAction() { actionType = AIActionType.Heal, cost = a.ap, battleActionList = battleActionList });
                    }
                }
            }

            return AIActionList;
        }

        private List<AIAction> getAIBuffActions()
        {
            List<AIAction> aiActionList = new List<AIAction>();
            return AIActionList;
        }

        private List<AIAction> getAINukeActions()
        {
            List<AIAction> aiActionList = new List<AIAction>();
            return AIActionList;
        }

        private List<AIAction> getAIFleeActions()
        {
            List<AIAction> aiActionList = new List<AIAction>();
            return AIActionList;
        }

    }
}
