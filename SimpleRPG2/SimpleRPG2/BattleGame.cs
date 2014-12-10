using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRPG2
{
    public class BattleGame
    {
      
        public Board board;
        public List<GameCharacter> characterList;
        public int currentCharacter = 0;

        public BattleLog battleLog;

        public Random r;

        public int TurnCounter;
        public bool NewTurn;

        public GameCharacter ActiveCharacter
        {
            get
            {
                return characterList[currentCharacter];
            }
        }

        public Tile ActiveTile
        {
            get
            {
                return board.getTileFromLocation(ActiveCharacter.x, ActiveCharacter.y);
            }
        }

        public BattleGame()
        {
            r = new Random();
            battleLog = new BattleLog();

            StartBattle();
            
        }

     
        private void InitChars()
        {
            characterList = new List<GameCharacter>();
            characterList.Add(CharacterFactory.getPlayerCharacter(r));
            characterList.Add(CharacterFactory.getEnemy(r));
            characterList.Add(CharacterFactory.getEnemy(r));
            characterList.Add(CharacterFactory.getEnemy(r));

            battleLog.AddEntry("Characters Initialized");
        }

        private void StartBattle()
        {
            TurnCounter = 1;
            NewTurn = true;
            battleLog.AddEntry("Starting Battle");

            board = BoardFactory.getRandomBoard(this, 20);

            InitChars();
            SetBattleInitiative();
            placeCharactersInBoard();
            RunBattle();

        }

        private void SetBattleInitiative()
        { 
            //randomize attack order 
        }

        //Increment Initiative
        private void NextTurn()
        {
            ActiveCharacter.ResetAP();
            TurnCounter++;
            NewTurn = true;

            currentCharacter++;
            if(currentCharacter >= characterList.Count)
            {
                currentCharacter = 0;
            }
        }

        //currently, randomize
        private void placeCharactersInBoard()
        {
            foreach(var gc in characterList)
            {
                var freeTile = board.getFreeTile();
                board.FillTile(gc, freeTile);
            }
        }


        private void RunBattle()
        {
            bool isAction = false;
            BattleStatusType battleStatus = getBattleStatus();
            while(battleStatus == BattleStatusType.Running)
            {
                isAction = false;
                Console.Clear();
                Console.WriteLine(board.ToString());
                battleLog.Print(1);
                DisplayCharList();
                DisplayActiveChar();

                if (ActiveCharacter.hp > 0)
                {
                    if(NewTurn)
                    {
                        NewTurn = false;
                        ActiveCharacter.RunActiveEffects(this);
                    }

                    if (ActiveCharacter.type == CharacterType.Player)
                    {
                        isAction = DisplayMainMenu();

                    }
                    else
                    {
                        RunEnemyTurn();
                        System.Threading.Thread.Sleep(1000);
                    }

                    if (isAction)
                    {
                        DisplayTempBoard();
                        System.Threading.Thread.Sleep(1000);
                    }

                }
                else
                {
                    NextTurn();
                }

                battleStatus = getBattleStatus();
            }
            if(battleStatus == BattleStatusType.PlayersDead)
            {
                LoseBattle();
            }
            else if(battleStatus == BattleStatusType.EnemiesDead)
            {
                WinBattle();
            }
           
        }

        private void DisplayTempBoard()
        {
            Console.Clear();
            Console.WriteLine(board.ToStringTemp());
            battleLog.Print(1);
            DisplayCharList();
            DisplayActiveChar();

            board.ClearTempTiles();
        }

        //display the list of characters, indicating active
        private void DisplayCharList()
        {
            string txt = "";
            int counter = 0;
            foreach(var c in characterList)
            {
                if(counter == currentCharacter)
                {
                    txt += string.Format(" ->{0} ({1})<- ", c.name, c.displayChar);
                }
                else
                {
                    txt += string.Format(" {0} ({1}) ", c.name, c.displayChar);
                }
                counter++;
            }

            Console.WriteLine(txt);
        }

        private void DisplayActiveChar()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine(ActiveCharacter.ToString());
            Console.WriteLine("----------------------------");

        }

        //Return true if the player takes an Action
        private bool DisplayMainMenu()
        {
            bool isAction = false;

            List<string> menu = new List<string>(){"1. View","2. Move",
                "3. Move To","4. Attack", "5. Ranged Attack","6. Use Item",
                "7. Use Ability", "8. End Turn", "9. Refresh"};
            int input = CoreHelper.displayMenuGetInt(menu);
            switch(input)
            {
                case 1:
                    DisplayViewMenu();
                    break;
                case 2:
                    DisplayMoveMenu();
                    isAction = true;
                    break;
                case 3:
                    DisplayMoveToMenu();
                    isAction = true;
                    break;
                case 4:
                    DisplayAttackMenu();
                    isAction = true;
                    break;
                case 5:
                    DisplayRangedAttackMenu();
                    isAction = true;
                    break;
                case 6:
                    DisplayItemMenu();
                    isAction = true;
                    break;
                case 7:
                    DisplayAbilityMenu();
                    isAction = true;
                    break;

                case 8:
                    PlayerSkip();
                    break;
                case 9:
                    break;
                default: break;
            }
            return isAction;
        }

        private void DisplayViewMenu()
        {

            Console.Write(battleLog.ToString());

            //get list of characters
            List<string> displayCharList = new List<string>();
            int count = 1;
            foreach(var c in characterList)
            {
                displayCharList.Add(string.Format("{0}.{1} ({2})", count, c.name, c.displayChar));
                count++;
            }

            int input = CoreHelper.displayMenuGetInt(displayCharList);
            
            //Display the Character
            Console.Write(characterList[input - 1].ToString());
            Console.Write(">");
            Console.ReadLine();
            return;
        }

        private void DisplayMoveMenu()
        {
            List<string> menu = new List<string>() { "1. North","2.South","3.West","4.East","5.Back" };
            int input = CoreHelper.displayMenuGetInt(menu);
            var curTile = board.getTileFromLocation(ActiveCharacter.x,ActiveCharacter.y);
            DirectionType dir;
            switch(input)
            {
                case 1:
                    dir = DirectionType.North;
                    break;
                case 2:
                    dir = DirectionType.South;
                    break;
                case 3:
                    dir = DirectionType.West;
                     break;
                case 4:
                     dir = DirectionType.East;
                     break;
                case 5:
                     return;
                default:
                     return;
            }

            if (board.MoveCharacter(ActiveCharacter, board.getAdjascentTile(curTile, dir)))
            {
                battleLog.AddEntry(string.Format("{0} moved {1}.", ActiveCharacter.name, dir.ToString()));
            }
            else
            {
                battleLog.AddEntry(string.Format("{0} was unable to move {1}.", ActiveCharacter.name, dir.ToString()));
            }
        }

        private void DisplayMoveToMenu()
        {
            List<string> menu = new List<string>() { "Enter destination ex: 'A,1'" };
            bool valid = false;
            while (!valid)
            {
                string input = CoreHelper.displayMenuGetStr(menu);

                Point p = CoreHelper.parseStringPoint(input);
                if(p != null)
                {
                    if(PlayerMove(ActiveCharacter,p.x,p.y))
                    { 
                        valid = true;
                        battleLog.AddEntry(string.Format("{0} moved to {1},{2}", ActiveCharacter.name, p.x, p.y));
                    }
                    else
                    {
                        battleLog.AddEntry(string.Format("{0} unable to move to {1},{2}", ActiveCharacter.name, p.x, p.y));
                    }

                }

            }
        }

        private void DisplayRangedAttackMenu()
        {
            List<string> menu = new List<string>() { "Enter destination ex: 'A,1'" };
            bool valid = false;
            while (!valid)
            {
                string input = CoreHelper.displayMenuGetStr(menu);

                Point p = CoreHelper.parseStringPoint(input);
                if (p != null)
                {
                    PlayerRangedAttack(ActiveCharacter, board.getTileFromLocation(p.x, p.y));
                    valid = true;
                }

            }
        }


        private void DisplayAttackMenu()
        {

            //list enemies in range
            var attackCharList = getCharactersFromTileList(board.getTileListFromPattern(ActiveTile, TilePatternType.FourAdj));

            List<string> menu = new List<string>();
            int counter = 1;
            foreach (var c in attackCharList)
            {
                menu.Add(string.Format("{0}. {1} ({2})", counter, c.name, c.displayChar));
                counter++;
            }

            menu.Add(string.Format("{0}. Back", counter));

            int input = CoreHelper.displayMenuGetInt(menu);

            if (input != counter)
            {
                PlayerAttack(ActiveCharacter, attackCharList[input - 1]);

                board.AddTempChar(board.getTileFromLocation(attackCharList[input - 1].x, attackCharList[input - 1].y),'X');
            }
        }

        public void DisplayItemMenu()
        {

            List<string> itemList = new List<string>();
            int counter = 1;

            var usableItemList = (from data in ActiveCharacter.inventory
                                 where data is UsableItem
                                 select data).ToList();

            foreach(var i in usableItemList)
            {
                if(i is UsableItem)
                {
                    itemList.Add(string.Format("{0}. {1}",counter,i.name));
                    counter++;
                }
            }


            int input = CoreHelper.displayMenuGetInt(itemList);
            UsableItem tempItem = (UsableItem)usableItemList[input - 1];
            UseItem(ActiveCharacter, tempItem);


            return;
        }

        public void DisplayAbilityMenu()
        {

            List<string> displayList = new List<string>();
            int counter = 1;

            var usableAbilityList = (from data in ActiveCharacter.abilityList
                                    where data.uses > 0
                                    select data).ToList();

            foreach (var i in usableAbilityList)
            {
                displayList.Add(string.Format("{0}. {1}", counter, i.name));
                    counter++;
            }

            int input = CoreHelper.displayMenuGetInt(displayList);

            if (input > 0 && input <= usableAbilityList.Count)
            {

                bool valid = false;
                while (!valid)
                {
                    string targetTile = CoreHelper.displayMenuGetStr(new List<string>() { "Enter Target: (ex: A,1)" });

                    Point p = CoreHelper.parseStringPoint(targetTile);
                    if (p != null)
                    {
                        UseAbility(ActiveCharacter, usableAbilityList[input - 1], board.getTileFromLocation(p.x, p.y));

                        valid = true;
                    }
                }
            }

            return;
        }

        public GameCharacter getCharacterFromTile(Tile t)
        {
            foreach (var c in characterList)
            {
                if (c.x == t.x && c.y == t.y)
                {
                    return c;
                }
            }
            return null;
        }

        //return the list of chars at this tile list
        public List<GameCharacter> getCharactersFromTileList(List<Tile> tileList)
        {
            List<GameCharacter> retvalList = new List<GameCharacter>();
            foreach(var t in tileList)
            {
                foreach(var c in characterList)
                {
                    if(c.x == t.x && c.y == t.y)
                    {
                        retvalList.Add(c);
                    }
                }
            }

            return retvalList;
        }

        private BattleStatusType getBattleStatus()
        {
            bool playersDead = true;
            bool enemiesDead = true;
            foreach(var gc in characterList)
            {
                if(gc.type == CharacterType.Player)
                {
                    if(gc.hp > 0)
                    {
                        playersDead = false;
                    }
                }
                if(gc.type == CharacterType.Enemy)
                {
                    if(gc.hp > 0)
                    {
                        enemiesDead = false;
                    }
                }
            }

            if(playersDead)
            {
                return BattleStatusType.PlayersDead;
            }
            else if(enemiesDead)
            {
                return BattleStatusType.EnemiesDead;
            }
            else
            {
                return BattleStatusType.Running;
            }
        }

        private void PlayerSkip()
        {
            battleLog.AddEntry(characterList[currentCharacter].name + " ended turn.");

            NextTurn();

        }

        private bool PlayerMove(GameCharacter player, int x, int y)
        {
            return board.MoveCharacter(player, board.getTileFromLocation(x, y));
        }

        private void PlayerRangedAttack(GameCharacter player, Tile destination)
        {
            GameCharacter enemy = getCharacterFromTile(destination);

            if (enemy != null)
            {

                List<Tile> tileLOSList = board.getBoardLOS(ActiveTile, destination);

        
                foreach(var t in tileLOSList)
                {
                    board.AddTempChar(t, '*');
                }
               

                if (tileLOSList[tileLOSList.Count - 1] == destination)
                {
                    if (player.SpendAP(player.weapon.actionPoints))
                    {
                        CombatHelper.Attack(player, enemy, battleLog, r);
                    }
                }
                else
                {
                    string path = "";
                    foreach (var p in tileLOSList)
                    {
                        path += p.ToString() + " ";
                    }
                    battleLog.AddEntry("Ranged attack failed. Path: " + path);
                }
            }
        }

        private void PlayerAttack(GameCharacter player, GameCharacter enemy)
        {
            if (player.SpendAP(player.weapon.actionPoints))
            {
                CombatHelper.Attack(player, enemy, battleLog, r);
            }
        }

        public void UseItem(GameCharacter character, UsableItem item)
        {
            if(character.SpendAP(item.actionPoints))
            {
                item.uses--;
                if(item.uses <=0)
                {
                    //should this logic be here?
                    ActiveCharacter.inventory.Remove(item);
                }
                foreach(var a in item.activeEffects)
                {
                    character.AddActiveEffect(a,this);
                }

                battleLog.AddEntry(string.Format("{0} used item {1}", character.name, item.name));
            }
            else
            {
                battleLog.AddEntry(string.Format("{0} was unable to use item {1}",character.name, item.name));
            }

        }

        public void UseAbility(GameCharacter character, Ability ability, Tile target)
        {
            if(character.abilityList.Contains(ability))
            {
                if(AbilityHelper.UseAbility(character, ability, target, this))
                {
                    battleLog.AddEntry(string.Format("{0} used {1}",character.name,ability.name));
                }
                else
                {
                    battleLog.AddEntry(string.Format("{0} failed to use {1}", character.name, ability.name));
                }
            }
        }

   
        //enemy AI
        private void RunEnemyTurn()
        {
            AI.attackNearestPlayer(ActiveCharacter, this);
            NextTurn();

            //EnemySkip();
               
        }

        private void EnemySkip()
        {
            battleLog.AddEntry(string.Format("{0} ended its turn.",characterList[currentCharacter].name));

            NextTurn();
        }

        private void EnemyMove(GameCharacter enemy, int x, int y)
        {
            board.MoveCharacter(enemy, board.getTileFromLocation(x, y));
        }
        
        private void EnemyAttack(GameCharacter enemy, GameCharacter player)
        {
            CombatHelper.Attack(enemy, player, battleLog, r);
        }

        private void LoseBattle()
        {
            battleLog.AddEntry("Battle Lost");
            Console.WriteLine("You Lose! Press Enter to continue.\n");
            Console.ReadLine();
            StartBattle();
        }
        
        private void WinBattle()
        {

            battleLog.AddEntry("Battle Won");
            Console.WriteLine("You Win! Press Enter to continue.\n");
            Console.ReadLine();
            StartBattle();

        }


    }
}
